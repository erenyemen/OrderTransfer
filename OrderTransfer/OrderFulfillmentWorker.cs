using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.ChannelAdvisor;
using OrderTransfer.Helpers.TPLCentral;
using OrderTransfer.Models;
using OrderTransfer.Models.ChannelAdvisor;
using OrderTransfer.Models.Settings;
using OrderTransfer.Models.TPLCentral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderTransfer
{
    public class OrderFulfillmentWorker : BackgroundService
    {
        private readonly ILogger<OrderFulfillmentWorker> _logger;
        private readonly IConfiguration _config;
        private readonly IChannelAdvisorSettings _cadSetting;
        private readonly ITPLCentralSettings _tplSetting;
        private readonly IChannelAdvisorApiHelper _apiAdvisor;
        private readonly ITPLCentralApiHelper _apiCentral;
        private readonly TokenResult advisorToken;
        private readonly TokenResult centralToken;

        public OrderFulfillmentWorker(ILogger<OrderFulfillmentWorker> logger, 
            IConfiguration config, 
            IChannelAdvisorSettings cadSetting,
            ITPLCentralSettings tplSetting,
            IChannelAdvisorApiHelper apiAdvisor,
            ITPLCentralApiHelper apiCentral)
        {
            _logger = logger;
            _config = config;
            _cadSetting = cadSetting;
            _tplSetting = tplSetting;
            _apiAdvisor = apiAdvisor;
            _apiCentral = apiCentral;

            if (!_apiAdvisor.IsTokenExist)
                advisorToken = _apiAdvisor.GetToken();

            if (!_apiCentral.IsTokenExist)
                centralToken = _apiCentral.GetToken();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get Pending Shippment Orders from Channel Advisor.
                List<Order> resultOrders = _apiAdvisor.GetPendingOrders(); //.OrderByDescending(x => x.CreatedDateUtc).ToList();

                if (resultOrders.Count > 0)
                {
                    foreach (var item in resultOrders.OrderByDescending(x => x.CreatedDateUtc))
                    {
                        try
                        {
                            //TODO: Get tracking numbers from 3pl for order.
                            var res = _apiCentral.GetOrderByRefId<GetResponseObject>(item.SiteOrderID.Replace("#", ""));

                            if (res.Result is null) continue;
                            if (!res.IsSuccessful) continue;
                            if (res.Result.TotalResults == 0)
                            {
                                _logger.LogWarning($"No results were returned for order {item.SiteOrderID} from 3pl central");
                                continue;
                            }

                            var TrackingNumber = res.Result.ResourceList[0].RoutingInfo.TrackingNumber;

                            if (string.IsNullOrEmpty(TrackingNumber))
                            {
                                _logger.LogWarning($"The tracking number of the order number {res.Result.ResourceList[0].ReferenceNum} is null");
                                continue;
                            }

                            OrderShipped os = new OrderShipped()
                            {
                                Value = new Value()
                                {
                                    TrackingNumber = TrackingNumber,
                                    DistributionCenterID = item.Fulfillments[0].DistributionCenterID,
                                    DeliveryStatus = "Complete",
                                    ShippingCarrier = res.Result.ResourceList[0].RoutingInfo.Carrier
                                }
                            };

                            var resShipped = _apiAdvisor.PutOrderShipped<string>(item.ID, os);

                            if (resShipped.response.IsSuccessful)
                                _logger.LogInformation($"Tracking number send to Channel Advisor - {TrackingNumber}");
                        }
                        catch (Exception exp)
                        {
                            if (!string.IsNullOrEmpty(exp.Message))
                                _logger.LogError(exp.Message);
                        }
                    }
                }

                _logger.LogInformation("Order Fulfillment Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60 * _config.GetValue<int>("WorkingTime_FulfillmentWorker"), stoppingToken);
            }
        }
    }
}

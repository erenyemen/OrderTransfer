using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers;
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
using OrderTransfer.Helpers.Common;
namespace OrderTransfer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IChannelAdvisorSettings _cadSetting;
        private readonly ITPLCentralSettings _tplSetting;
        private readonly IChannelAdvisorApiHelper _apiAdvisor;
        private readonly ITPLCentralApiHelper _apiCentral;
        private readonly TokenResult advisorToken;
        private readonly TokenResult centralToken;

        public Worker(ILogger<Worker> logger, IChannelAdvisorSettings cadSetting,
            ITPLCentralSettings tplSetting,
            IChannelAdvisorApiHelper apiAdvisor,
            ITPLCentralApiHelper apiCentral)
        {
            _logger = logger;
            _cadSetting = cadSetting;
            _tplSetting = tplSetting;
            _apiAdvisor = apiAdvisor;
            _apiCentral = apiCentral;

            advisorToken = _apiAdvisor.GetToken();
            centralToken = _apiCentral.GetToken();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Order> listAdvisorOrders = GetOrdersByChannelAdvisor();

                foreach (var item in listAdvisorOrders.OrderByDescending(x => x.CreatedDateUtc))
                {
                    Root postObject = CreateTplCentralObject(item);

                    var res = _apiCentral.PostOrders(postObject);

                    if (res.IsSuccessful)
                    {
                        var resPut =_apiAdvisor.PutOrder(item.ID);
                        
                        //TODO: Gönderildiðine dair yapýlacak iþlemler
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Order> GetOrdersByChannelAdvisor()
        {
            return _apiAdvisor.GetOrders();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Root CreateTplCentralObject(Order item)
        {
            Root returnObject = new Root()
            {
                customerIdentifier = new CustomerIdentifier()
                {
                    Id = 1
                },
                facilityIdentifier = new FacilityIdentifier()
                {
                    Id = 1,
                    Name = "DropRight"
                },
                orderItems = OrderItemsMapper(item.Items),
                referenceNum = item.ID.ToString(),
                notes = item.PublicNotes,
                shippingNotes = item.PrivateNotes,
                billingCode = "Sender",//TODO: Bilgi Yok.
                routingInfo = RoutingInfoMapper(item.Fulfillments.FirstOrDefault()),
                shipTo = new ShipTo()
                {
                    address1 = item.ShippingAddressLine1,
                    address2 = item.ShippingAddressLine2,
                    city = item.ShippingCity,
                    companyName = item.ShippingCompanyName,
                    country = item.ShippingCountry,
                    name = $"{item.ShippingFirstName.ToStringByTrim()} {item.ShippingLastName.ToStringByTrim()}",
                    state = item.ShippingStateOrProvinceName,
                    zip = item.ShippingPostalCode
                }
            };

            return returnObject;
        }

        private List<Models.TPLCentral.OrderItem> OrderItemsMapper(List<Models.ChannelAdvisor.OrderItem> items)
        {
            List<Models.TPLCentral.OrderItem> result = new List<Models.TPLCentral.OrderItem>();

            Models.TPLCentral.OrderItem resultItem;
            foreach (var item in items)
            {
                resultItem = new Models.TPLCentral.OrderItem()
                {
                    //NOTE: SKU , Müþteri için 3PL Central'da ayarlananla tam olarak eþleþmelidir.
                    itemIdentifier = new ItemIdentifier() { /*Id = item.ID,*/ sku = item.Sku },
                    qty = item.Quantity
                };

                result.Add(resultItem);
            }

            return result;
        }

        private RoutingInfo RoutingInfoMapper(Fulfillment item)
        {
            if (item is null) return null;

            return new RoutingInfo()
            {
                //TODO: Bakýlacak tekrar
                carrier = item.ShippingCarrier,
                mode = ""
            };
        }
    }
}

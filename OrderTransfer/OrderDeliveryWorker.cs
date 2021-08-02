using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.ChannelAdvisor;
using OrderTransfer.Helpers.Common;
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
    // https://complete.channeladvisor.com/Orders/AllOrders.mvc/List?apid=12036490&filter=ldxJsmY-b9NIVUT34CeUc7ocCIk
    // https://developer.channeladvisor.com/working-with-orders/fulfillments/mark-an-existing-fulfillment-as-shipped
    // https://developer.3plcentral.com/#intro
    /// <summary>
    /// Channel Advisor'dan 3PL Central'a Sipariþleri Gönderen Servis.
    /// </summary>
    public class OrderDeliveryWorker : BackgroundService
    {
        private readonly ILogger<OrderDeliveryWorker> _logger;
        private readonly IConfiguration _config;
        private readonly IChannelAdvisorSettings _cadSetting;
        private readonly ITPLCentralSettings _tplSetting;
        private readonly IChannelAdvisorApiHelper _apiAdvisor;
        private readonly ITPLCentralApiHelper _apiCentral;
        private readonly TokenResult advisorToken;
        private readonly TokenResult centralToken;

        public OrderDeliveryWorker(ILogger<OrderDeliveryWorker> logger, IConfiguration config, IChannelAdvisorSettings cadSetting,
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
            _logger.LogInformation("Order Delivery Worker running at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                // Channel Advisor'dan Order bilgilerini getirir.
                List<Order> listAdvisorOrders = _apiAdvisor.GetOrders();

                foreach (var item in listAdvisorOrders.OrderByDescending(x => x.CreatedDateUtc))// OrderByDescending(x => x.CreatedDateUtc)
                {
                    // 3PL Cental' a sipariþ (order) gönderiliyor.
                    var res = _apiCentral.PostOrders<PostOrderResponse>(CreateTplCentralObject(item));

                    if (res.IsSuccessful)
                    {
                        #region Comment

                        //OrderConfirm oc = new OrderConfirm()
                        //{
                        //    confirmDate = DateTime.Now,
                        //    trackingNumber = "1212121312121",
                        //    recalcAutoCharges = false
                        //};
                        //// 3PL Central - Confirm Order 3PL Central
                        //var resConfirm = _apiCentral.PostOrderConfirm<string>(oc, res.Result.ReadOnly.OrderId, res.Etag);

                        #endregion Comment

                        // Channel Advisor' da sipariþ, 'Bekleyen Sevkiyat (Pending Shipment)' durumuna çekiliyor.
                        var resPut = _apiAdvisor.PutOrder<string>(item.ID);
                    }
                }

                _logger.LogInformation("Order Delivery Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60 * _config.GetValue<int>("WorkingTime_OrderDeliveryWorker"), stoppingToken);
            }
        }

        #region Mapper Methods

        private Root CreateTplCentralObject(Order item)
        {
            return new Root()
            {
                customerIdentifier = new CustomerIdentifier() { Id = 1 },
                facilityIdentifier = new FacilityIdentifier() { Id = 1, Name = "DropRight" },
                orderItems = OrderItemsMapper(item.Items),
                referenceNum = item.SiteOrderID.Replace("#",""),
                notes = item.PublicNotes,
                shippingNotes = item.PrivateNotes,
                billingCode = "Sender",//TODO: Bilgi Yok.
                routingInfo = RoutingInfoMapper(item.Fulfillments.FirstOrDefault()),
                shipTo = new ShipTo()
                {
                    address1 = item.ShippingAddressLine1,
                    address2 = item.ShippingAddressLine2,
                    city = item.ShippingCity,
                    companyName = string.IsNullOrEmpty(item.ShippingCompanyName) ? $"{item.ShippingFirstName.ToStringByTrim()} {item.ShippingLastName.ToStringByTrim()}" : item.ShippingCompanyName,
                    country = item.ShippingCountry,
                    name = $"{item.ShippingFirstName.ToStringByTrim()} {item.ShippingLastName.ToStringByTrim()}",
                    state = item.ShippingStateOrProvince,
                    zip = item.ShippingPostalCode,
                    phoneNumber = item.ShippingDaytimePhone,
                    emailAddress = item.BuyerEmailAddress
                }
            };
        }

        private List<Models.TPLCentral.HttpApi3plCentralComRelsOrdersItem> EmbededItemsMapper(List<Models.ChannelAdvisor.OrderItem> items)
        {
            List<Models.TPLCentral.HttpApi3plCentralComRelsOrdersItem> result = new List<Models.TPLCentral.HttpApi3plCentralComRelsOrdersItem>();

            Models.TPLCentral.HttpApi3plCentralComRelsOrdersItem resultItem;
            foreach (var item in items)
            {
                resultItem = new Models.TPLCentral.HttpApi3plCentralComRelsOrdersItem()
                {
                    //NOTE: SKU , Müþteri için 3PL Central'da ayarlananla tam olarak eþleþmelidir.
                    itemIdentifier = new ItemIdentifier() { /*Id = item.ID,*/ sku = item.Sku },
                    qty = item.Quantity
                };

                result.Add(resultItem);
            }

            return result;
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
            return new RoutingInfo() { carrier = item.ShippingCarrier, mode = item.ShippingClass };
        }

        #endregion Mapper Methods
    }
}
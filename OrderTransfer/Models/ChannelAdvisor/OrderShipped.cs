using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models.ChannelAdvisor
{
    //public class OrderShipped
    //{
    //    public string DeliveryStatus { get; set; }
    //    public string TrackingNumber { get; set; }
    //    public DateTime ShippedDateUtc { get; set; }
    //}

    public class Value
    {
        public DateTime ShippedDateUtc { get; set; }
        public string TrackingNumber { get; set; }
        public string SellerFulfillmentID { get; set; }
        public int DistributionCenterID { get; set; }
        public string DeliveryStatus { get; set; }
        public string ShippingCarrier { get; set; }
        public string ShippingClass { get; set; }
    }

    public class OrderShipped
    {
        public Value Value { get; set; }
    }
}

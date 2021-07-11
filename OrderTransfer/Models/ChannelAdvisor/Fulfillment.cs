using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models.ChannelAdvisor
{
    public class Fulfillment
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public int OrderID { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string Type { get; set; }
        public string DeliveryStatus { get; set; }
        public string TrackingNumber { get; set; }
        public object ReturnTrackingNumber { get; set; }
        public string ShippingCarrier { get; set; }
        public string ShippingClass { get; set; }
        public int DistributionCenterID { get; set; }
        public object ExternalFulfillmentCenterCode { get; set; }
        public string ExternalFulfillmentStatus { get; set; }
        public double ShippingCost { get; set; }
        public double InsuranceCost { get; set; }
        public double TaxCost { get; set; }
        public DateTime? ShippedDateUtc { get; set; }
        public object SellerFulfillmentID { get; set; }
        public bool HasShippingLabel { get; set; }
        public bool HasChannelPackingSlip { get; set; }
        public bool HasReturnLabel { get; set; }
        public bool HasChannelReturnLabel { get; set; }
        public object ExternalFulfillmentNumber { get; set; }
        public object ExternalFulfillmentReferenceNumber { get; set; }
        public object ShippingLabelRequestID { get; set; }
        public object StagingLocation { get; set; }
        public object LabelFormat { get; set; }
        public object ReturnLabelFormat { get; set; }
        public object ChannelReturnLabelFormat { get; set; }
    }
}

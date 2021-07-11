using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models.TPLCentral
{
    public class Root
    {
        public CustomerIdentifier customerIdentifier { get; set; }
        public FacilityIdentifier facilityIdentifier { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public string referenceNum { get; set; }
        public string billingCode { get; set; }
        public RoutingInfo routingInfo { get; set; }
        public ShipTo shipTo { get; set; }
        public string notes { get; set; }
        public string shippingNotes { get; set; }
    }
}
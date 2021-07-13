using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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

        //public Embedded _embedded { get; set; }
    }

    public class HttpApi3plCentralComRelsOrdersItem
    {
        public ItemIdentifier itemIdentifier { get; set; }
        public double qty { get; set; }
    }

    public class Embedded
    {
        [JsonPropertyName("http://api.3plCentral.com/rels/orders/item")]
        public List<HttpApi3plCentralComRelsOrdersItem> HttpApi3plCentralComRelsOrdersItem { get; set; }
    }
}
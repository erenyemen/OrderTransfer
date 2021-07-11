using System.Collections.Generic;

namespace OrderTransfer.Models.ChannelAdvisor
{
    public class OrderResponse
    {
        [Newtonsoft.Json.JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<Order> value { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "@odata.nextLink")]
        public string OdataNextLink { get; set; }
    }
}
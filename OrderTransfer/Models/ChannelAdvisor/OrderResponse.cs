using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrderTransfer.Models.ChannelAdvisor
{
    public class OrderResponse
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }
        public List<Order> value { get; set; }

        [JsonPropertyName("@odata.nextLink")]
        public string OdataNextLink { get; set; }
    }
}
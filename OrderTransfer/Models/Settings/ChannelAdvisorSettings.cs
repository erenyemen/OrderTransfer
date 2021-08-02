namespace OrderTransfer.Models.Settings
{
    public class ChannelAdvisorSettings : IChannelAdvisorSettings
    {
        public string BaseURL { get; set; }
        public string GetTOKEN_URL { get; set; }
        public string GetDistributionCenter_URL { get; set; }
        public string GetOrders_URL { get; set; }
        public string GetFulfillments_URL { get; set; }
        public IdentitySettings IdentityInfo { get; set; }
        public string PutOrder_URL { get; set; }
        public string PutOrderShipped_URL { get; set; }
        public string GetPendingOrder_URL { get; set; }
    }
}
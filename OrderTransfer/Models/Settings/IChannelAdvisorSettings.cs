namespace OrderTransfer.Models.Settings
{
    public interface IChannelAdvisorSettings : IRestApiSettings
    {
        public string GetDistributionCenter_URL { get; set; }
        public string GetFulfillments_URL { get; set; }
        public string PutOrder_URL { get; set; }
    }
}
namespace OrderTransfer.Models.Settings
{
    public interface ITPLCentralSettings : IRestApiSettings
    {
        public string PostOrder_URL { get; set; }

        public string PostOrderConfirm_URL { get; set; }
        public string GetTrackingNum_URL { get; set; }
    }
}
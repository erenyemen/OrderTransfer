namespace OrderTransfer.Models.Settings
{
    public class TPLCentralSettings : ITPLCentralSettings
    {
        public string BaseURL { get; set; }
        public IdentitySettings IdentityInfo { get; set; }
        public string GetTOKEN_URL { get; set; }
        public string PostOrder_URL { get; set; }
        public string GetOrders_URL { get; set; }
    }
}
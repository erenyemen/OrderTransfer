namespace OrderTransfer.Models.Settings
{
    public interface IRestApiSettings
    {
        public string BaseURL { get; set; }
        public string GetTOKEN_URL { get; set; }
        public string GetOrders_URL { get; set; }
        public IdentitySettings IdentityInfo { get; set; }
    }
}
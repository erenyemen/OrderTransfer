namespace OrderTransfer.Models.Settings
{
    public interface ITPLCentralSettings : IRestApiSettings
    {
        public string PostOrder_URL { get; set; }
    }
}
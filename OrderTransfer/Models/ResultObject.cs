using RestSharp;

namespace OrderTransfer.Models
{
    public class ResultObject<T>
    {
        public bool IsSuccessful { get; set; }
        public string Content { get; set; }
        public string Etag { get; set; }
        public T Result { get; set; }
        public IRestResponse response { get; set; }
        public string ErrorMessage { get; set; }
    }
}

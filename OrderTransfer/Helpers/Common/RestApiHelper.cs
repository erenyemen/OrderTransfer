using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.Serializer;
using OrderTransfer.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace OrderTransfer.Helpers.Common
{
    public abstract class RestApiHelper
    {
        public ResultObject<T> CallApi<T,logObject>(string url, RestRequest request, ILogger<logObject> logger, 
            HttpBasicAuthenticator basicAuth = null) where T: class
        {
            ResultObject<T> result;

            var client = new RestClient(url) { Timeout = -1 };

            if (basicAuth != null) client.Authenticator = basicAuth;

            IRestResponse response = client.Executer(request, logger);

            result = new ResultObject<T>()
            {
                response = response,
                IsSuccessful = response.IsSuccessful,
                Content = response.Content,
                Etag = GetETaginHeader(response),
                Result = JsonHelper.Deserialize<T, logObject>(response.Content, logger)
            };

            return result;
        }

        public string GetETaginHeader(IRestResponse response)
        {
            string result = string.Empty;

            foreach (var item in response.Headers)
            {
                if (item.Name == "ETag")
                {
                    result = item.Value.ToString(); break;
                }
            }

            return result;
        }
    }
}
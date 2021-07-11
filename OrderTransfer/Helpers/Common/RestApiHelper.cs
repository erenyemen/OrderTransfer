using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.Serializer;
using OrderTransfer.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace OrderTransfer.Helpers.Common
{
    public abstract class RestApiHelper
    {
        public virtual T CallApi<T,logObject>(string url, RestRequest request, ILogger<logObject> logger) where T : class
        {
            var client = new RestClient(url);
            client.Timeout = -1;

            IRestResponse response = client.Executer<logObject>(request, logger);

            response.Content = response.Content.Replace("@odata.nextLink", "OdataNextLink").Replace("@odata.context", "OdataContext");

            T responseObject = JsonHelper.Deserialize<T, logObject>(response.Content, logger);

            return responseObject;
        }

        public virtual T CallApiBasicAuth<T, logObject>(string url, HttpBasicAuthenticator basicAuth, RestRequest request, ILogger<logObject> logger) where T : class where logObject : class
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            client.Authenticator = basicAuth;

            IRestResponse response = client.Executer<logObject>(request, logger);

            T responseObject = JsonHelper.Deserialize<T, logObject>(response.Content, logger);

            return responseObject;
        }

        public ResultObject CallApi<logObject>(string url, RestRequest request, ILogger<logObject> logger)
        {
            ResultObject result = new ResultObject();

            var client = new RestClient(url);
            client.Timeout = -1;

            IRestResponse response = client.Executer<logObject>(request, logger);

            result.IsSuccessful = response.IsSuccessful;
            result.Content = response.Content;

            return result;
        }
    }
}
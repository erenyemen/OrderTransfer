using Microsoft.Extensions.Logging;
using RestSharp;

namespace OrderTransfer.Helpers.Common
{
    public static class Extentions
    {
        public static IRestResponse Executer<logObject>(this RestClient client, RestRequest request, ILogger<logObject> logger)
        {
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                logger.LogError($"{response.StatusCode.ToString()}, Error Detail: {(response.ErrorMessage == null ? response.Content : response.ErrorMessage)}");

            return response;
        }

        public static string ToStringByTrim(this string item)
        {
            return item == null ? string.Empty : item.Trim();
        }
    }
}

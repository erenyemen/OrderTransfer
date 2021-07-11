using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace OrderTransfer.Helpers.Serializer
{
    public static class JsonHelper
    {
        public static string Serialize(object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);

            return jsonString;
        }

        public static T Deserialize<T, logObject>(string jsonString, ILogger<logObject> logger) where T : class
        {
            T result;

            try
            {
                result = JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }

            return result;
        }
    }
}

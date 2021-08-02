using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.Common;
using OrderTransfer.Models;
using OrderTransfer.Models.ChannelAdvisor;
using OrderTransfer.Models.Settings;
using RestSharp;
using System;
using System.Collections.Generic;

namespace OrderTransfer.Helpers.ChannelAdvisor
{
    public class ChannelAdvisorApiHelper : RestApiHelper, IChannelAdvisorApiHelper
    {
        private readonly ILogger<ChannelAdvisorApiHelper> _logger;
        private IChannelAdvisorSettings _settings { get; set; }
        private TokenResult Token { get; set; }

        public bool IsTokenExist { get { return Token == null ? false : true; } }

        public ChannelAdvisorApiHelper(ILogger<ChannelAdvisorApiHelper> logger, IChannelAdvisorSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public TokenResult GetToken()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", _settings.IdentityInfo.Client_Id);
            request.AddParameter("client_secret", _settings.IdentityInfo.Client_Secret);
            request.AddParameter("grant_type", "soap");
            request.AddParameter("developer_key", _settings.IdentityInfo.Developer_Key);
            request.AddParameter("password", _settings.IdentityInfo.Password);
            request.AddParameter("account_id", _settings.IdentityInfo.Account_Id);
            request.AddParameter("scope", _settings.IdentityInfo.Scope);

            string url = $"{_settings.BaseURL}{_settings.GetTOKEN_URL}";
            var result = CallApi<TokenResult, ChannelAdvisorApiHelper>(url, request, _logger);//CallApiDeserialize

            if (result != null && result.Result.access_token != null)
                _logger.LogInformation($"GetToken: {result.Result.access_token}");

            Token = result.Result;
            Token.AccessTokenCreatedDate = DateTime.Now;
            return result.Result;
        }

        public TokenResult GetRefreshToken()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", _settings.IdentityInfo.RefreshToken);

            string url = $"{_settings.BaseURL}{_settings.GetTOKEN_URL}";
            var result = CallApi<TokenResult, ChannelAdvisorApiHelper>(url, request, _logger);//CallApiDeserialize

            if (result != null && result.Result.access_token != null)
                _logger.LogInformation($"GetRefreshToken: {result.Result.access_token}");

            Token = result.Result;
            Token.AccessTokenCreatedDate = DateTime.Now;
            return result.Result;
        }

        public List<Order> GetOrders()
        {
            if (Token.IsExpired)
                GetToken();

            List<Order> result;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            var responseObject = CallApi<OrderResponse, ChannelAdvisorApiHelper>($"{_settings.BaseURL}{_settings.GetOrders_URL}", request, _logger);
            result = responseObject.Result.value;

            // paging
            if (!string.IsNullOrEmpty(responseObject.Result.OdataNextLink))
            {
                OrderResponse responseOdataNextLink;
                var nextLink = responseObject.Result.OdataNextLink;
                do
                {
                    responseOdataNextLink = CallApi<OrderResponse, ChannelAdvisorApiHelper>(nextLink, request, _logger).Result;
                    result.AddRange(responseOdataNextLink.value);

                    nextLink = responseOdataNextLink.OdataNextLink;

                } while (!string.IsNullOrEmpty(responseOdataNextLink.OdataNextLink));
            }

            _logger.LogInformation($"Orders Count: {result.Count}");

            return result;
        }

        public ResultObject<T> PutOrder<T>(int orderId) where T : class
        {
            if (Token.IsExpired)
                GetToken();

            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            string jsonBody = @"{""ShippingStatus"":""PendingShipment""}";
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            string url = $"{_settings.BaseURL}{_settings.PutOrder_URL}";
            url = string.Format(url, orderId);

            var response = CallApi<T, ChannelAdvisorApiHelper>(url, request, _logger);

            return response;
        }

        public ResultObject<T> PutOrderShipped<T>(int fulfillmentID, OrderShipped orderShip) where T : class
        {
            if (Token.IsExpired)
                GetToken();

            var request = new RestRequest(Method.POST);//PUT
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            string tempResult = orderShip.Serialize();
            request.AddParameter("application/json", orderShip.Serialize(), ParameterType.RequestBody);

            var url = string.Format($"{_settings.BaseURL}{_settings.PutOrderShipped_URL}", fulfillmentID);

            var response = CallApi<T, ChannelAdvisorApiHelper>(url, request, _logger);

            return response;
        }

        public List<Order> GetPendingOrders()
        {
            if (Token.IsExpired)
                GetToken();

            List<Order> result;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            var responseObject = CallApi<OrderResponse, ChannelAdvisorApiHelper>($"{_settings.BaseURL}{_settings.GetPendingOrder_URL}", request, _logger);
            result = responseObject.Result.value;

            // paging
            if (!string.IsNullOrEmpty(responseObject.Result.OdataNextLink))
            {
                OrderResponse responseOdataNextLink;
                var nextLink = responseObject.Result.OdataNextLink;
                do
                {
                    responseOdataNextLink = CallApi<OrderResponse, ChannelAdvisorApiHelper>(nextLink, request, _logger).Result;
                    result.AddRange(responseOdataNextLink.value);

                    nextLink = responseOdataNextLink.OdataNextLink;

                } while (!string.IsNullOrEmpty(responseOdataNextLink.OdataNextLink));
            }

            _logger.LogInformation($"Pending Orders Count: {result.Count}");

            return result;
        }
    }
}
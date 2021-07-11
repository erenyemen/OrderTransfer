using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.Common;
using OrderTransfer.Models;
using OrderTransfer.Models.ChannelAdvisor;
using OrderTransfer.Models.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Helpers.ChannelAdvisor
{
    public class ChannelAdvisorApiHelper : RestApiHelper, IChannelAdvisorApiHelper
    {
        private readonly ILogger<ChannelAdvisorApiHelper> _logger;
        private IChannelAdvisorSettings _settings { get; set; }
        private TokenResult Token { get; set; }

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

            var result = CallApi<TokenResult, ChannelAdvisorApiHelper>(url, request, _logger);

            if (result != null && result.access_token != null)
                _logger.LogInformation($"GetToken: {result.access_token}");

            Token = result;
            return result;
        }

        public TokenResult GetRefreshToken()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", _settings.IdentityInfo.RefreshToken);

            string url = $"{_settings.BaseURL}{_settings.GetTOKEN_URL}";
            var result = CallApi<TokenResult, ChannelAdvisorApiHelper>(url, request, _logger);

            if (result != null && result.access_token != null)
                _logger.LogInformation($"GetRefreshToken: {result.access_token}");

            return result;
        }

        public List<Order> GetOrders()
        {
            List<Order> result;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            var responseObject = CallApi<OrderResponse, ChannelAdvisorApiHelper>($"{_settings.BaseURL}{_settings.GetOrders_URL}", request, _logger);
            result = responseObject.value;

            // Pages
            if (!string.IsNullOrEmpty(responseObject.OdataNextLink))
            {
                OrderResponse responseOdataNextLink;
                var nextLink = responseObject.OdataNextLink;
                do
                {
                    responseOdataNextLink = CallApi<OrderResponse, ChannelAdvisorApiHelper>(nextLink, request, _logger);
                    result.AddRange(responseOdataNextLink.value);

                    nextLink = responseOdataNextLink.OdataNextLink;

                } while (!string.IsNullOrEmpty(responseOdataNextLink.OdataNextLink));
            }

            return result;
        }

        public ResultObject PutOrder(int orderId)
        {
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            string jsonBody = @"{""ShippingStatus"":""PendingShipment""}";
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            string url = $"{_settings.BaseURL}{_settings.PutOrder_URL}";
            url = string.Format(url, orderId);

            var response = CallApi<ChannelAdvisorApiHelper>(url, request, _logger);

            return response;
        }
    }
}
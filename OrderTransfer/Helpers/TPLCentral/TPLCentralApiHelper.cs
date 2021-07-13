using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.Common;
using OrderTransfer.Models;
using OrderTransfer.Models.Settings;
using OrderTransfer.Models.TPLCentral;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace OrderTransfer.Helpers.TPLCentral
{
    /// <summary>
    /// 3PL CENTRAL REST APIS
    /// </summary>
    public class TPLCentralApiHelper : RestApiHelper, ITPLCentralApiHelper
    {
        private readonly ILogger<TPLCentralApiHelper> _logger;
        private readonly ITPLCentralSettings _settings;
        private TokenResult Token { get; set; }

        public TPLCentralApiHelper(ILogger<TPLCentralApiHelper> logger, ITPLCentralSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public TokenResult GetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public TokenResult GetToken()
        {
            TokenResult result;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", _settings.IdentityInfo.Client_Id);
            request.AddParameter("client_secret", _settings.IdentityInfo.Client_Secret);
            request.AddParameter("user_login_id", _settings.IdentityInfo.user_login_id);
            request.AddParameter("grant_type", "client_credentials");

            string url = $"{_settings.BaseURL}{_settings.GetTOKEN_URL}";
            var basicAuth = new HttpBasicAuthenticator(_settings.IdentityInfo.Client_Id, _settings.IdentityInfo.Client_Secret);

            result = CallApi<TokenResult, TPLCentralApiHelper>(url, request, _logger, basicAuth).Result;

            if (result != null && result.access_token != null)
                _logger.LogInformation($"GetToken: {result.access_token}");

            Token = result;
            Token.AccessTokenCreatedDate = DateTime.Now;
            return result;
        }

        public ResultObject<T> PostOrders<T>(Root order) where T: class
        {
            if (Token.IsExpired)
                GetToken();

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", order.Serialize(), ParameterType.RequestBody);

            string url = $"{_settings.BaseURL}{_settings.PostOrder_URL}";
            var response = CallApi<T, TPLCentralApiHelper>(url, request, _logger);

            return response;
        }

        public ResultObject<T> PostOrderConfirm<T>(OrderConfirm confirm, int orderId, string eTag) where T : class
        {
            if (Token.IsExpired)
                GetToken();

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("if-match", eTag);
            request.AddParameter("application/json", confirm.Serialize(), ParameterType.RequestBody);

            string url = $"{_settings.BaseURL}{string.Format(_settings.PostOrderConfirm_URL, orderId)}";
            var response = CallApi<T, TPLCentralApiHelper>(url, request, _logger);

            return response;
        }
    }
}
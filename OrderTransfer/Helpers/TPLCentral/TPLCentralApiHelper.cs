using Microsoft.Extensions.Logging;
using OrderTransfer.Helpers.ChannelAdvisor;
using OrderTransfer.Helpers.Common;
using OrderTransfer.Helpers.Serializer;
using OrderTransfer.Models;
using OrderTransfer.Models.Settings;
using OrderTransfer.Models.TPLCentral;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

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

            result = CallApiBasicAuth<TokenResult, TPLCentralApiHelper>(url, basicAuth, request, _logger);

            if (result != null && result.access_token != null)
                _logger.LogInformation($"GetToken: {result.access_token}");

            Token = result;
            return result;
        }

        public ResultObject PostOrders(Root order)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {Token.access_token}");
            request.AddHeader("Content-Type", "application/json");

            string jsonUser = JsonHelper.Serialize(order);
            request.AddParameter("application/json", jsonUser, ParameterType.RequestBody);

            string url = $"{_settings.BaseURL}{_settings.PostOrder_URL}";

            var response = CallApi<TPLCentralApiHelper>(url, request, _logger);

            return response;
        }
    }
}
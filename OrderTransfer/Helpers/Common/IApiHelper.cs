using OrderTransfer.Models;

namespace OrderTransfer.Helpers.Common
{
    public interface IApiHelper
    {
        public TokenResult GetToken();

        public TokenResult GetRefreshToken();

        public bool IsTokenExist { get; }
    }
}
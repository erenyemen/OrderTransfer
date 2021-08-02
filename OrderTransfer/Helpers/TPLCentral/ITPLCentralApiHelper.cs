using OrderTransfer.Helpers.Common;
using OrderTransfer.Models;
using OrderTransfer.Models.TPLCentral;

namespace OrderTransfer.Helpers.TPLCentral
{
    public interface ITPLCentralApiHelper : IApiHelper
    {
        public ResultObject<T> PostOrders<T>(Root order) where T : class;
        public ResultObject<T> PostOrderConfirm<T>(OrderConfirm confirm, int orderId, string eTag) where T : class;
        public ResultObject<T> GetOrderByRefId<T>(string referanceNumber) where T : class;
    }
}
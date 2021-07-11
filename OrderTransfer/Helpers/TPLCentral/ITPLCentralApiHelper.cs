using OrderTransfer.Helpers.Common;
using OrderTransfer.Models;
using OrderTransfer.Models.TPLCentral;

namespace OrderTransfer.Helpers.TPLCentral
{
    public interface ITPLCentralApiHelper : IApiHelper
    {
        public ResultObject PostOrders(Root order);
    }
}
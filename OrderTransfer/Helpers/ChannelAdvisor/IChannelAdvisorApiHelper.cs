using OrderTransfer.Models.ChannelAdvisor;
using OrderTransfer.Helpers.Common;
using System.Collections.Generic;
using OrderTransfer.Models;

namespace OrderTransfer.Helpers.ChannelAdvisor
{
    public interface IChannelAdvisorApiHelper : IApiHelper
    {
        public List<Order> GetOrders();

        public ResultObject PutOrder(int orderId);
    }
}
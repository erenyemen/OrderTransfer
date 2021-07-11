using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models.ChannelAdvisor
{
    public class OrderItemBundleComponent
    {
        public int OrderItemID { get; set; }
        public int ProfileID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int BundleProductID { get; set; }
        public string Sku { get; set; }
        public string BundleSku { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
    }
}

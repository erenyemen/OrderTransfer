using System.Collections.Generic;

namespace OrderTransfer.Models.ChannelAdvisor
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string SiteOrderItemID { get; set; }
        public object SellerOrderItemID { get; set; }
        public string SiteListingID { get; set; }
        public string Sku { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TaxPrice { get; set; }
        public double ShippingPrice { get; set; }
        public double ShippingTaxPrice { get; set; }
        public double RecyclingFee { get; set; }
        public object UnitEstimatedShippingCost { get; set; }
        public object GiftMessage { get; set; }
        public object GiftNotes { get; set; }
        public double GiftPrice { get; set; }
        public double GiftTaxPrice { get; set; }
        public bool IsBundle { get; set; }
        public string ItemURL { get; set; }
        public object HarmonizedCode { get; set; }
        public List<OrderItemBundleComponent> BundleComponents { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace OrderTransfer.Models.ChannelAdvisor
{
    public class Order
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public int UserDataPresent { get; set; }
        public object UserDataRemovalDateUTC { get; set; }
        public int? SiteAccountID { get; set; }
        public string SiteOrderID { get; set; }
        public string SecondarySiteOrderID { get; set; }
        public string SellerOrderID { get; set; }
        public object CheckoutSourceID { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ImportDateUtc { get; set; }
        public string PublicNotes { get; set; }
        public string PrivateNotes { get; set; }
        public string SpecialInstructions { get; set; }
        public double TotalPrice { get; set; }
        public double TotalTaxPrice { get; set; }
        public double TotalShippingPrice { get; set; }
        public double TotalShippingTaxPrice { get; set; }
        public double TotalInsurancePrice { get; set; }
        public double TotalGiftOptionPrice { get; set; }
        public double TotalGiftOptionTaxPrice { get; set; }
        public double AdditionalCostOrDiscount { get; set; }
        public DateTime? EstimatedShipDateUtc { get; set; }
        public DateTime? DeliverByDateUtc { get; set; }
        public string RequestedShippingCarrier { get; set; }
        public string RequestedShippingClass { get; set; }
        public object ResellerID { get; set; }
        public int FlagID { get; set; }
        public object FlagDescription { get; set; }
        public string OrderTags { get; set; }
        public string DistributionCenterTypeRollup { get; set; }
        public string CheckoutStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingStatus { get; set; }
        public DateTime? CheckoutDateUtc { get; set; }
        public DateTime? PaymentDateUtc { get; set; }
        public DateTime? ShippingDateUtc { get; set; }
        public string BuyerUserId { get; set; }
        public string BuyerEmailAddress { get; set; }
        public bool BuyerEmailOptIn { get; set; }
        public string OrderTaxType { get; set; }
        public string ShippingTaxType { get; set; }
        public string GiftOptionsTaxType { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTransactionID { get; set; }
        public object PaymentPaypalAccountID { get; set; }
        public string PaymentCreditCardLast4 { get; set; }
        public string PaymentMerchantReferenceNumber { get; set; }
        public object ShippingTitle { get; set; }
        public string ShippingFirstName { get; set; }
        public string ShippingLastName { get; set; }
        public object ShippingSuffix { get; set; }
        public string ShippingCompanyName { get; set; }
        public object ShippingCompanyJobTitle { get; set; }
        public string ShippingDaytimePhone { get; set; }
        public object ShippingEveningPhone { get; set; }
        public string ShippingAddressLine1 { get; set; }
        public string ShippingAddressLine2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingStateOrProvince { get; set; }
        public string ShippingStateOrProvinceName { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingCountry { get; set; }
        public object BillingTitle { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingSuffix { get; set; }
        public string BillingCompanyName { get; set; }
        public object BillingCompanyJobTitle { get; set; }
        public string BillingDaytimePhone { get; set; }
        public string BillingEveningPhone { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingStateOrProvince { get; set; }
        public string BillingStateOrProvinceName { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCountry { get; set; }
        public object PromotionCode { get; set; }
        public double PromotionAmount { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<Fulfillment> Fulfillments { get; set; }
    }
}
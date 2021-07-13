using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models.TPLCentral
{
    public class CreatedByIdentifier
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class LastModifiedByIdentifier
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class ReadOnly
    {
        public int OrderId { get; set; }
        public int AsnCandidate { get; set; }
        public int RouteCandidate { get; set; }
        public bool FullyAllocated { get; set; }
        public bool DeferNotification { get; set; }
        public bool IsClosed { get; set; }
        public int LoadedState { get; set; }
        public bool RouteSent { get; set; }
        public bool AsnSent { get; set; }
        public List<object> Packages { get; set; }
        public List<object> OutboundSerialNumbers { get; set; }
        public int ParcelLabelType { get; set; }
        public CustomerIdentifier CustomerIdentifier { get; set; }
        public FacilityIdentifier FacilityIdentifier { get; set; }
        public int WarehouseTransactionSourceType { get; set; }
        public int TransactionEntryType { get; set; }
        public DateTime CreationDate { get; set; }
        public CreatedByIdentifier CreatedByIdentifier { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public LastModifiedByIdentifier LastModifiedByIdentifier { get; set; }
        public int Status { get; set; }
        public int OrderItemId { get; set; }
        public UnitIdentifier UnitIdentifier { get; set; }
        public double OriginalPrimaryQty { get; set; }
        public bool IsOrderQtySecondary { get; set; }
        public List<object> Allocations { get; set; }
        public string RowVersion { get; set; }
    }

    public class UnitIdentifier
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Post_OrderItem
    {
        public ReadOnly ReadOnly { get; set; }
        public ItemIdentifier ItemIdentifier { get; set; }
        public string Qualifier { get; set; }
        public double Qty { get; set; }
        public List<object> SavedElements { get; set; }
        public List<object> _links { get; set; }
    }

    public class Post_RoutingInfo
    {
        public bool IsCod { get; set; }
        public bool IsInsurance { get; set; }
        public bool RequiresDeliveryConf { get; set; }
        public bool RequiresReturnReceipt { get; set; }
        public string Carrier { get; set; }
        public string Mode { get; set; }
    }

    public class Billing
    {
    }

    public class FulfillInvInfo
    {
    }

    public class Post_ShipTo
    {
        public bool IsQuickLookup { get; set; }
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public int AddressStatus { get; set; }
    }

    public class ParcelOption
    {
        public int OrderId { get; set; }
        public string DeliveryConfirmationType { get; set; }
        public int DeliveredDutyPaid { get; set; }
        public double DryIceWeight { get; set; }
        public double InsuranceAmount { get; set; }
        public int InsuranceType { get; set; }
        public string InternationalContentsType { get; set; }
        public string InternationalNonDelivery { get; set; }
        public bool ResidentialFlag { get; set; }
        public bool SaturdayDeliveryFlag { get; set; }
    }

    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public bool IsTemplated { get; set; }
    }

    public class PostOrderResponse
    {
        public ReadOnly ReadOnly { get; set; }
        public List<Post_OrderItem> OrderItems { get; set; }
        public string ReferenceNum { get; set; }
        public string Notes { get; set; }
        public double NumUnits1 { get; set; }
        public double TotalWeight { get; set; }
        public double TotalVolume { get; set; }
        public string BillingCode { get; set; }
        public bool AddFreightToCod { get; set; }
        public bool UpsIsResidential { get; set; }
        public string ShippingNotes { get; set; }
        public FulfillInvInfo FulfillInvInfo { get; set; }
        public Post_RoutingInfo RoutingInfo { get; set; }
        public Billing Billing { get; set; }
        public Post_ShipTo ShipTo { get; set; }
        public List<object> SavedElements { get; set; }
        public ParcelOption ParcelOption { get; set; }
        public List<Link> _links { get; set; }
    }
}

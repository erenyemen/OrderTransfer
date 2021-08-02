using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Models.TPLCentral
{
    class GetOrderResponse
    {
    }

    public class GetResponse_ReadOnly
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

  

    public class GetResponse_OrderItem
    {
        public GetResponse_ReadOnly ReadOnly { get; set; }
        public ItemIdentifier ItemIdentifier { get; set; }
        public string Qualifier { get; set; }
        public double Qty { get; set; }
        public List<object> SavedElements { get; set; }
        public List<object> _links { get; set; }
    }

    public class ResourceList
    {
        public GetResponse_ReadOnly ReadOnly { get; set; }
        public List<GetResponse_OrderItem> OrderItems { get; set; }
        public string ReferenceNum { get; set; }
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
        public ShipTo ShipTo { get; set; }
        public List<object> SavedElements { get; set; }
        public ParcelOption ParcelOption { get; set; }
        public List<object> _links { get; set; }
    }

    public class GetResponseObject
    {
        public int TotalResults { get; set; }
        public List<ResourceList> ResourceList { get; set; }
        public List<object> _links { get; set; }
    }


}

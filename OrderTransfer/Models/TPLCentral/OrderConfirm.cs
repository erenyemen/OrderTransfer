using System;

namespace OrderTransfer.Models.TPLCentral
{
    public class OrderConfirm
    {
        public DateTime confirmDate { get; set; }
        public string trackingNumber { get; set; }
        public bool recalcAutoCharges { get; set; }
    }
}
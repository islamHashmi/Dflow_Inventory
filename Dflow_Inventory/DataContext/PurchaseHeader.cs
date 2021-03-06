//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dflow_Inventory.DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseHeader
    {
        public PurchaseHeader()
        {
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
        }
    
        public int purchaseId { get; set; }
        public string poNumber { get; set; }
        public System.DateTime purchaseDate { get; set; }
        public string orderNumber { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public int vendorId { get; set; }
        public Nullable<decimal> totalQuantity { get; set; }
        public Nullable<decimal> totalAmount { get; set; }
        public string remark { get; set; }
        public string finYear { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual VendorMaster VendorMaster { get; set; }
    }
}

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
    
    public partial class ItemMaster
    {
        public ItemMaster()
        {
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
            this.productionDetails = new HashSet<productionDetail>();
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
            this.Stocks = new HashSet<Stock>();
        }
    
        public int itemId { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public Nullable<int> unitId { get; set; }
        public Nullable<decimal> sellingPrice { get; set; }
        public Nullable<decimal> openingStock { get; set; }
        public Nullable<decimal> currentStock { get; set; }
        public bool rawMaterial { get; set; }
        public bool active { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual UnitMaster UnitMaster { get; set; }
        public virtual ICollection<productionDetail> productionDetails { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}

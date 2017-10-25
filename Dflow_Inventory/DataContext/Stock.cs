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
    
    public partial class Stock
    {
        public long stockId { get; set; }
        public System.DateTime stockDate { get; set; }
        public Nullable<int> invoiceId { get; set; }
        public Nullable<int> purchaseId { get; set; }
        public int itemId { get; set; }
        public string stockType { get; set; }
        public Nullable<decimal> openingStock { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public Nullable<decimal> closingStock { get; set; }
        public string remark { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
    
        public virtual Item_Master ItemMaster { get; set; }
        public virtual StockType StockType1 { get; set; }
    }
}
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
    
    public partial class InvoiceDetail
    {
        public int invoiceDetailId { get; set; }
        public int invoiceId { get; set; }
        public int itemId { get; set; }
        public string unit { get; set; }
        public string hsnCode { get; set; }
        public Nullable<decimal> rate { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public Nullable<decimal> amount { get; set; }
    
        public virtual InvoiceHeader InvoiceHeader { get; set; }
        public virtual Item_Master ItemMaster { get; set; }
    }
}
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
    
    public partial class sp_Stock_Report_Detail_Result
    {
        public long stockId { get; set; }
        public System.DateTime stockDate { get; set; }
        public Nullable<long> invoiceId { get; set; }
        public Nullable<long> purchaseId { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string stockTypeDescription { get; set; }
        public Nullable<decimal> openingStock { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public Nullable<decimal> closingStock { get; set; }
        public string narration { get; set; }
        public string remark { get; set; }
    }
}

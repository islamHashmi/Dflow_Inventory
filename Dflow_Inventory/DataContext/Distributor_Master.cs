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
    
    public partial class Distributor_Master
    {
        public int distributorId { get; set; }
        public string distributorCode { get; set; }
        public string distributorName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string officeNo { get; set; }
        public string mobileNo { get; set; }
        public string faxNo { get; set; }
        public string emailId { get; set; }
        public string gstNo { get; set; }
        public string panNo { get; set; }
        public bool active { get; set; }
        public int entryBy { get; set; }
        public Nullable<System.DateTime> entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    }
}

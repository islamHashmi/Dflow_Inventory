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
    
    public partial class productionHeader
    {
        public productionHeader()
        {
            this.productionDetails = new HashSet<productionDetail>();
        }
    
        public long productionId { get; set; }
        public System.DateTime productionDate { get; set; }
        public string batchNo { get; set; }
        public decimal totalQuantity { get; set; }
        public string remark { get; set; }
        public string finYear { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual ICollection<productionDetail> productionDetails { get; set; }
    }
}

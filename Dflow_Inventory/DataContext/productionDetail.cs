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
    
    public partial class productionDetail
    {
        public long productionDetailId { get; set; }
        public long productionId { get; set; }
        public int itemId { get; set; }
        public decimal quantity { get; set; }
    
        public virtual ItemMaster ItemMaster { get; set; }
        public virtual productionHeader productionHeader { get; set; }
    }
}

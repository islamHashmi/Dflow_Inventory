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
    
    public partial class UnitMaster
    {
        public UnitMaster()
        {
            this.ItemMasters = new HashSet<ItemMaster>();
        }
    
        public int unitId { get; set; }
        public string unitName { get; set; }
        public string unitCode { get; set; }
        public bool active { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual ICollection<ItemMaster> ItemMasters { get; set; }
    }
}

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
    
    public partial class Designation
    {
        public Designation()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int designationId { get; set; }
        public string designationName { get; set; }
        public bool active { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
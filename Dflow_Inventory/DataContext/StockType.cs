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
    
    public partial class StockType
    {
        public StockType()
        {
            this.Stocks = new HashSet<Stock>();
        }
    
        public string stockTypeCode { get; set; }
        public string stockTypeDescription { get; set; }
    
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}

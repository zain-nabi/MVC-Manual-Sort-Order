//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Amalfi_Sort_Order.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class metric
    {
        public int metricID { get; set; }
        public string description { get; set; }
        public Nullable<int> campaignID { get; set; }
        public Nullable<int> display_order { get; set; }
    }
}
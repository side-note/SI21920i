//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF_TP2_52D_14_1920i
{
    using System;
    using System.Collections.Generic;
    
    public partial class Email
    {
        public int code { get; set; }
        public string description { get; set; }
        public string addr { get; set; }
        public Nullable<decimal> nif { get; set; }
    
        public virtual Client Client { get; set; }
    }
}

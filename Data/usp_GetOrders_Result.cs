//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    
    public partial class usp_GetOrders_Result
    {
        public long ID { get; set; }
        public System.DateTime DATETIME { get; set; }
        public string OBSERVATIONS { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSTOMERCUIT { get; set; }
        public Nullable<long> CUSTOMERINTERNALID { get; set; }
        public Nullable<decimal> DISCOUNT1 { get; set; }
        public string OBSERVATIONS1 { get; set; }
        public Nullable<long> RECEIPTTYPE { get; set; }
    }
}
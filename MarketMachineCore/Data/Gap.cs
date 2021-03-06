//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketMachineCore.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gap
    {
        public int GapId { get; set; }
        public int MarketId { get; set; }
        public System.DateTime OpenDate { get; set; }
        public string Direction { get; set; }
        public decimal GapTop { get; set; }
        public decimal GapBottom { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public decimal GapExtension { get; set; }
        public Nullable<System.DateTime> ExtendDate { get; set; }
        public Nullable<System.DateTime> FillDate { get; set; }
        public int DailyCandleId { get; set; }
        public decimal GapSize { get; set; }
    
        public virtual Market Market { get; set; }
        public virtual DailyCandle DailyCandle { get; set; }
    }
}

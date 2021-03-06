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
    
    public partial class Market
    {
        public Market()
        {
            this.Gaps = new HashSet<Gap>();
            this.Candles = new HashSet<Candle>();
            this.DailyCandles = new HashSet<DailyCandle>();
        }
    
        public int MarketId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int MarketTypeId { get; set; }
        public int ExchangeId { get; set; }
        public Nullable<decimal> CurrentPrice { get; set; }
        public string GapsCurrentDirection { get; set; }
        public Nullable<int> GapsCurrentDirectionCount { get; set; }
        public Nullable<int> GapsTotalOpen { get; set; }
        public Nullable<decimal> GapsFirstPrice { get; set; }
        public Nullable<System.DateTime> DateFirstCandle { get; set; }
        public Nullable<System.DateTime> DateLastCandle { get; set; }
    
        public virtual ICollection<Gap> Gaps { get; set; }
        public virtual MarketType MarketType { get; set; }
        public virtual ICollection<Candle> Candles { get; set; }
        public virtual ICollection<DailyCandle> DailyCandles { get; set; }
    }
}

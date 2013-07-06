//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarkMachine.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Market
    {
        public Market()
        {
            this.DailyCandles = new HashSet<DailyCandle>();
            this.Gaps = new HashSet<Gap>();
        }
    
        public int MarketId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ExchangeId { get; set; }
    
        public virtual ICollection<DailyCandle> DailyCandles { get; set; }
        public virtual ICollection<Gap> Gaps { get; set; }
    }
}

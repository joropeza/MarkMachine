using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkMachine.Models
{
    public class Candle
    {
        public DateTime CandleStart { get; set; }
        public DateTime CandleEnd { get; set; }
        public string Period { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        
    }
}
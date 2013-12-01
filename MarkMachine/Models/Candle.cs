using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarkMachine.Models
{
    public class Candle
    {
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CandleStart { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CandleEnd { get; set; }
        public string Period { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public string Badges { get; set; }

        public int? Volume { get; set; }

        public string BiasText { get; set; }

        public string DayStoryText { get; set; }
        public string DaySetupText { get; set; }
        public string ITText { get; set; }
        
    }
}
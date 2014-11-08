using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketMachineCore.Data;

namespace MarketMachineProcedures
{
    public class ItemCount
    {
        public string Item { get; set; }
        public int Count { get; set; }
    }

    public class DataIntegrityChecks
    {
        public void DailyCandleCheck(int marketId)
        {
            using (MarketsDBEntities mde = new MarketsDBEntities())
            {
                //sum each month

                var candles = mde.DailyCandles.Where(x => x.MarketId == marketId).OrderBy(x=>x.Date).Select(y => y.Date.Year.ToString() + "/" + y.Date.Month.ToString());

                var candleCounts = candles.GroupBy(x => x).Select(x => new ItemCount { Item = x.Key, Count = x.Count()});

                foreach (var candle in candleCounts)
                {
                    Console.WriteLine(candle.Item + " " + candle.Count.ToString());
                }
            }
        }
    }
}

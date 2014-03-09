using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketMachineCore.Data;



namespace MarketMachineProcedures
{
    class CandleProcesses
    {

        marketdbEntities mdb = new marketdbEntities();

        public void CatchUpDailyCandles(int MarketId) {


            //load the current daily candles

            var CompiledCandles = mdb.Candles.Where(x => x.MarketId == MarketId && x.PeriodId == 5);

            //load from the data

            var RawData = mdb.DailyCandles.Where(x => x.MarketId == MarketId);


            //compare lengths, if same then return

            Console.WriteLine(CompiledCandles.Count());
            Console.WriteLine(RawData.Count());

            if (CompiledCandles.Count() < RawData.Count())
            {

                //if not same, need to add some candles
                HashSet<DateTime> processedDates = new HashSet<DateTime>(CompiledCandles.Select(s => s.StartDate));

                var results = RawData.Where(m => !processedDates.Contains(m.Date));

                foreach (var dc in results)
                {
                    Candle c = new Candle();
                    c.Close = dc.Close;
                    c.EndDate = dc.Date;
                    c.High = dc.High;
                    c.Low = dc.Low;
                    c.MarketId = dc.MarketId;
                    c.Open = dc.Open;
                    c.PeriodId = 5;
                    c.StartDate = dc.Date;
                    c.Volume = dc.Volume;

                    using (marketdbEntities mdbSaver = new marketdbEntities())
                    {

                        mdbSaver.Candles.Add(c);
                        mdbSaver.SaveChanges();
                    }

                }
            }



        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketMachineCore.Data;

namespace MarketMachineProcedures
{
    public class ProcessRunner
    {

        public void GetCurrentPrices()
        {
            using (MarketsDBEntities mde = new MarketsDBEntities()) 
			{
                foreach (var market in mde.Markets)
                {
                    using (MarketsDBEntities mdeSaver = new MarketsDBEntities())
                    {
                        var lastCandle = mdeSaver.DailyCandles.Where(x => x.MarketId == market.MarketId).OrderByDescending(x => x.Date).FirstOrDefault();
                        market.CurrentPrice = lastCandle.Close;
                        
                        Console.WriteLine("Updated price for " + market.Symbol);
                    }
                }

                mde.SaveChanges();

            }


        }


		public void GapFinder (int marketId)
		{
			using (MarketsDBEntities mde = new MarketsDBEntities()) 
			{
			//find all candles that don't have a corresponding gap
				var candles = mde.DailyCandles.Where (x => x.MarketId == marketId && x.Gaps.Count == 0);

				foreach (var candle in candles.OrderBy(x=>x.Date))
                {
                    Console.WriteLine("Finding gap for " + candle.Date.ToShortDateString());

                    //1. Find the candle one candle back date-wise

                    int trys = 0;
                    DailyCandle lastCandle = new DailyCandle();
                    DateTime lastDate = candle.Date.AddDays(-1);
                    while (trys < 6 && lastCandle.Date< DateTime.Now.AddYears(-100))
                    {
                        //Console.WriteLine(lastDate.Date.ToShortDateString());

                        if (mde.DailyCandles.Where(x => x.MarketId == marketId && x.Date == lastDate).Count() > 0)
                        {
                            Console.WriteLine("Last candle found.");
                            lastCandle = mde.DailyCandles.Where(x => x.MarketId == marketId && x.Date == lastDate).First();
                        }
                        trys++;
                        lastDate = lastDate.AddDays(-1);
                    }

                    if (lastCandle.Date< DateTime.Now.AddYears(-100))
                        Console.WriteLine("No last candle found!");

                    Console.WriteLine("Last candle: " + lastCandle.Date.ToShortDateString());

                    Gap g = new Gap();
                    g.MarketId = marketId;
                    g.DailyCandleId = candle.DailyCandleId;
                    g.OpenDate = candle.Date;
                    g.GapSize = candle.Open - lastCandle.Close;

                    if (candle.Open >= lastCandle.Close)
                    {
                        g.Direction = "Up";
                        g.GapTop = candle.Open;
                        g.GapBottom = lastCandle.Close;
                        

                        if (candle.Low <= lastCandle.Close)
                        {
                            g.FillDate = candle.Date;
                        }
                    }
                    else
                    {
                        g.Direction = "Down";
                        g.GapTop = lastCandle.Close;
                        g.GapBottom = candle.Open;

                        if (candle.High >= lastCandle.Close)
                        {
                            g.FillDate = candle.Date;
                        }
                    }

                    g.GapExtension = candle.Open + g.GapSize;

                    using (MarketsDBEntities mdeAdder = new MarketsDBEntities())
                    {
                        mdeAdder.Gaps.Add(g);
                        mdeAdder.SaveChanges();
                    }

                   
                    


				}


			}
		}

        public void UpdateMarkets()
        {
            MarketUpdates mu = new MarketUpdates();

            using (MarketsDBEntities mde = new MarketsDBEntities()) 
            {
                foreach (var m in mde.Markets) { 
                mu.UpdateMarketHistorical(DateTime.Now.AddYears(-1), DateTime.Now, m.MarketId);
                }
            }

        }

        public void UpdateMarket(int MarketId)
        {
            MarketUpdates mu = new MarketUpdates();
            mu.UpdateMarketHistorical(DateTime.Now.AddYears(-1), DateTime.Now, MarketId);

        }

        public void CandleCatchupAll()
        {
            //grab the list of currently active markets
            List<int> MarketList = new List<int>();
            MarketList.Add(1);

            foreach (var m in MarketList)
            {
                CandleCatchupMarket(m);

            }
            


        }


        public void CandleCatchupMarket(int MarketId)
        {
            CandleProcesses cp = new CandleProcesses();

            //make sure each period's candles have been entered

            cp.CatchUpDailyCandles(MarketId);            

            //catch up the current W and M candles





        }
    }
}

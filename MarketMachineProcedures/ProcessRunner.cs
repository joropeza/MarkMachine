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

        public void FillUnfilledGaps()
        {
            using (MarketsDBEntities mde = new MarketsDBEntities())
            {
                foreach (var gap in mde.Gaps.Where(x => x.FillDate == null))
                {
                    var candles = mde.DailyCandles.Where(x => x.MarketId == gap.MarketId && x.Date > gap.OpenDate);
                    foreach (var candle in candles)
                    {
                        if (gap.Direction == "Up")
                        {
                            if (candle.Low <= gap.GapBottom)
                            {
                                Console.WriteLine("Gap fill on " + candle.Date.ToShortDateString());
                                gap.FillDate = candle.Date;
                                break;
                            }


                        }
                        else
                        {
                            if (candle.High >= gap.GapTop)
                            {
                                Console.WriteLine("Gap fill on " + candle.Date.ToShortDateString());
                                gap.FillDate = candle.Date;
                                break;
                            }
                        }
                    }
                }

                mde.SaveChanges();
            }
        }

        public void FirstAndLastDates()
        {
            using (MarketsDBEntities mde = new MarketsDBEntities())
            {
                foreach (var m in mde.Markets)
                {
                    m.DateFirstCandle = m.DailyCandles.OrderBy(x => x.Date).First().Date;
                    m.DateLastCandle = m.DailyCandles.OrderByDescending(x => x.Date).First().Date;
                    
                }

                mde.SaveChanges();
            }

        }

        public void GapStats()
        {
            using (MarketsDBEntities mde = new MarketsDBEntities())
            {
                foreach (var m in mde.Markets)
                {
                    int marketId = m.MarketId;
                    //get all unfilled gaps
                    var market = mde.Markets.Find(marketId);
                    var gaps = market.Gaps.Where(x => x.MarketId == marketId && x.FillDate == null);


                    //ordered by date, which direction is the last unfilled gap?
                    var lastGap = gaps.OrderByDescending(x => x.OpenDate).FirstOrDefault();

                    if (lastGap.OpenDate > DateTime.Now.AddYears(-100))
                    {
                        market.GapsCurrentDirection = lastGap.Direction;
                        if (lastGap.Direction == "Up")
                            market.GapsFirstPrice = lastGap.GapTop;
                        else
                            market.GapsFirstPrice = lastGap.GapBottom;
                    }

                    
                }

                mde.SaveChanges();

            }
        }

        public void GapFinders()
        {
            MarketUpdates mu = new MarketUpdates();

            using (MarketsDBEntities mde = new MarketsDBEntities())
            {
                foreach (var m in mde.Markets)
                {
                    GapFinder(m.MarketId);
                }
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
                    Console.WriteLine("Updating data for " + m.Name);
                mu.UpdateMarketHistorical(DateTime.Now.AddYears(-5), DateTime.Now, m.MarketId);
                }
            }

        }

        public void UpdateMarket(int MarketId)
        {
            MarketUpdates mu = new MarketUpdates();
            mu.UpdateMarketHistorical(DateTime.Now.AddYears(-1), DateTime.Now, MarketId);

        }

        public void GenerateMonthlyCandles(int marketId)
        {
            using (MarketsDBEntities mde = new MarketsDBEntities())
            {
                var candles = mde.DailyCandles.Where(x => x.MarketId == marketId)
                    .OrderBy(x => x.Date)
                    .GroupBy(x => x.Date.Month.ToString() + "-" + x.Date.Year.ToString())
                    .Select(x => new CandleListItem { MarketId = marketId,
                        StartDate = x.Min(y => y.Date),
                        High = x.Max(y => y.High),
                        Low = x.Min(y => y.High),
                        Open = 0,
                        Close = 0,
                        Volume = x.Sum(y => y.Volume),
                    
                    });


                foreach (CandleListItem candle in candles)
                {
                    //get open and close

                    var dailyCandles = mde.DailyCandles.Where(x => x.MarketId == marketId && x.Date.Month == candle.StartDate.Month && x.Date.Year == candle.StartDate.Year).OrderBy(x => x.Date);
                    candle.Open = dailyCandles.First().Open;
                    candle.Close = dailyCandles.OrderByDescending(x => x.Date).First().Close;

                    Candle c = new Candle();
                    c.Close = candle.Close;
                    c.EndDate = dailyCandles.OrderByDescending(x => x.Date).First().Date;
                    c.High = candle.High;
                    c.Low = candle.Low;
                    c.MarketId = marketId;
                    c.Open = candle.Open;
                    c.PeriodId = 2;
                    c.StartDate = candle.StartDate;
                    c.Volume = candle.Volume;

                    if (mde.Candles.Where(x=>x.MarketId == marketId && x.StartDate == c.StartDate && x.PeriodId == c.PeriodId).Count() == 0)
                    {
                        using (MarketsDBEntities mdeSaver = new MarketsDBEntities())
                        {
                            mdeSaver.Candles.Add(c);
                            mdeSaver.SaveChanges();
                        }
                    }

                    Console.WriteLine(candle.StartDate.ToShortDateString() + " H:" + candle.High);
                }
                
            }
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

    public class CandleListItem
    {
        public int MarketId { get; set; }
        public DateTime StartDate { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Open { get; set; }
        public Decimal Close { get; set; }
        public Int64 Volume { get; set; }



    }
    
}

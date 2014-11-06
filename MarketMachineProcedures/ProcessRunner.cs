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
		public void GapFinder (int marketId)
		{
			using (MarketsDBEntities mde = new MarketsDBEntities()) 
			{
			//find all candles that don't have a corresponding gap
				var candles = mde.DailyCandles.Where (x => x.MarketId == marketId);

				foreach (var candle in candles.OrderBy(x=>x.Date){




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

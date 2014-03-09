using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMachineProcedures
{
    public class ProcessRunner
    {

        public void CandleCatchupAll()
        {
            //grab the list of currently active markets
            List<int> MarketList = new List<int>();
            MarketList.Add(2);

            foreach (var m in MarketList)
            {
                CandleCatchupMarket(m);

            }
            


        }


        public void CandleCatchupMarket(int MarketId)
        {
            CandleProcesses cp = new CandleProcesses();

            //make sure each period's candles have been entered

            cp.CatchUpDailyCandles(2);            

            //catch up the current W and M candles





        }
    }
}

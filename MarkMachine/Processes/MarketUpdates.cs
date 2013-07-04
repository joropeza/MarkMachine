using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarkMachine.Data;

namespace MarketMachineClassLibrary
{
    public class MarketUpdates
    {
        marketdbEntities mde = new marketdbEntities();

        
       

        public void UpdateMarketHistorical(DateTime sd, DateTime ed, int MarketId)
        {
                     
           
            YahooFinance yf = new YahooFinance();

            Market m = mde.Markets.Where(x => x.MarketId == MarketId).FirstOrDefault();

            List<DailyCandle> dcs = yf.GetHistoricalData(sd, ed, m);

            foreach (DailyCandle td in dcs)
            {
                //see if this day is in the db for this issue

                int x = (from oa in mde.DailyCandles
                         where oa.MarketId == MarketId && oa.Date == td.Date
                         select oa).Count<DailyCandle>();

                if (x == 0)
                {
                    mde.DailyCandles.Add(td);
                }

                //if yes, no nothing for now (consider overwriting at a certain point?)

                //if no, insert this row into the table

            }

            mde.SaveChanges();

           
        }

        /*

        public void UpdateMarkets(List<Market> lom)
        {
            //grab a list of active markets
                       


            //iterate the list, looking for mkts that yahoo understands. 

            List<Market> GetThese = new List<Market>();

            foreach (Market mm in lom)
            {
                if (mm.MarketType.CanLookup == "Y")
                {
                    //check the market to see when the last observation was. if it wasn't when a new observation was recorded, add to lookup list

                    //check if there's ever been an Obs for this market. If not, we need to do one for sure

                    if (dba.ObservationGetAllForThisMarket(mm).Count > 0)
                    {

                        DateTime LatestODate = dba.ObservationGetMostRecent(mm.Id).Date;
                        DateTime WouldBeDate = dba.GetClosestMarketHalfHour();

                        DateTime L1 = LatestODate.AddMinutes(20);

                        if (WouldBeDate > L1) //this means that the would-be observation would be more than 20 mins later than the current one
                        {
                            //add this bastard to the list!

                            GetThese.Add(mm);
                        }
                    }
                    else
                    {
                        GetThese.Add(mm);
                    }

                }



            }



            //send lookup request to lookup func, 100 markets at a time

            YahooFinance yf = new YahooFinance();
            List<List<string>> listall = new List<List<string>>();

            List<string> listx = new List<string>();



            foreach (Market mm in GetThese)
            {
                listx.Add(mm.MarketName);
                if (listx.Count == 100)
                {
                    List<string> listnew = new List<string>(listx);
                    listall.Add(listnew);
                    listx.Clear();
                }
            }


            foreach (List<string> listeach in listall)
            {

                List<QuickQuote> ql = yf.GetQuote(listeach);

                foreach (QuickQuote q in ql)
                {
                    foreach (Market mm in GetThese)
                    {
                        if (q.Symbol == mm.MarketName)
                        {
                            if (mm.MarketDescription == null)
                            {
                                dba.MarketUpdateDescription(mm.Id, q.StockName);


                            }

                            dba.ObservationAdd(mm.Id, dba.GetClosestMarketHalfHour(), q.Quote);
                            if (dba.GetClosestMarketHalfHour().Hour == 13 && DateTime.Now.Hour > 13)
                            {
                                //if it's after 1:30, let's check to see if this day is in the TradingDays yet (it shouldn't be). If not, let's add it

                                if (!dba.TradingDayExistsForThisMarket(mm.Id, DateTime.Parse(DateTime.Now.ToShortDateString())))
                                {

                                    dba.TradingDayAdd(mm.Id, DateTime.Parse(DateTime.Now.ToShortDateString()), q.OpenPrice, q.Quote, q.HighPrice, q.LowPrice, q.Volume);
                                }

                            }
                        }

                    }

                }

            }






            //iterate results, make observations for each market

            //reload the page

        }

        */
        
    }
}
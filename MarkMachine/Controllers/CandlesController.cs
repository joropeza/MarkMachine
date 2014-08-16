using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkMachine.Models;


namespace MarkMachine.Controllers
{
    public class CandlesController : Controller
    {


        public ActionResult Details(int MarketId)
        {
            DateTime sd = DateTime.Now.AddMonths(-1);
            DateTime ed = DateTime.Now;

            using (MarketMachineCore.Data.MarketsDBEntities dba = new MarketMachineCore.Data.MarketsDBEntities())
            {

                var dc = dba.Markets.Find(MarketId).DailyCandles.Where(i => i.Date >= sd);

                List<Candle> candles = new List<Candle>();

                foreach (var d in dc)
                {
                    Candle c = new Candle();
                    c.CandleStart = d.Date;
                    c.CandleEnd = d.Date;
                    c.High = d.High;
                    c.Low = d.Low;
                    c.Close = d.Close;
                    c.Open = d.Open;
                    c.Volume = d.Volume;
                    candles.Add(c);
                }

                return View(candles);

            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MarketMachineCore.Data;

namespace MarkMachine.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            MarketMachineClassLibrary.MarketUpdates mu = new MarketMachineClassLibrary.MarketUpdates();

            marketdbEntities mdb = new marketdbEntities();

            foreach (Market m in mdb.Markets.Where(x=>x.CategoryId>1))
            {

                mu.UpdateMarketHistorical(DateTime.Now.AddDays(-180), DateTime.Now, m.MarketId);
            }

            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkMachine.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            MarketMachineClassLibrary.MarketUpdates mu = new MarketMachineClassLibrary.MarketUpdates();

            mu.UpdateMarketHistorical(DateTime.Now.AddDays(-90), DateTime.Now, 3);

            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MarkMachine.Models;

namespace MarkMachine.Controllers
{
    public class RadChartController : Controller
    {
        //
        // GET: /RadChart/

        public ActionResult Image(int MarketId = 1)
        {
            RadChart rc = new RadChart();
            rc.CreateChart(MarketId);

            var cd = new System.Net.Mime.ContentDisposition
            {

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());



            return File(rc.ChartData, "image/jpg");

        }

        public ActionResult Index(int MarketId = 1)
        {

            // return File(rc.ChartData, "image/jpg");

            return View(MarketId);
        }

    }
}

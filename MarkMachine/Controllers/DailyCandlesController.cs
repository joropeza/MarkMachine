using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketMachineCore.Data;

namespace MarkMachine.Controllers
{
    public class DailyCandlesController : Controller
    {
        private marketdbEntities db = new marketdbEntities();

        //
        // GET: /DailyCandles/Index/1

        public ActionResult Index(int id = 1)
        {
            var dailycandles = db.DailyCandles.Include(d => d.Market).Where(x => x.Market.MarketId == id);
            ViewBag.MarketName = dailycandles.First().Market.Name;
            return View(dailycandles.ToList());
        }

        //
        // GET: /DailyCandles/Details/5

        public ActionResult Details(int id = 1)
        {
            DailyCandle dailycandle = db.DailyCandles.Find(id);
            if (dailycandle == null)
            {
                return HttpNotFound();
            }
            return View(dailycandle);
        }

        //
        // GET: /DailyCandles/Create

        public ActionResult Create()
        {
            ViewBag.MarketId = new SelectList(db.Markets, "MarketId", "Symbol");
            return View();
        }

        //
        // POST: /DailyCandles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DailyCandle dailycandle)
        {
            if (ModelState.IsValid)
            {
                db.DailyCandles.Add(dailycandle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarketId = new SelectList(db.Markets, "MarketId", "Symbol", dailycandle.MarketId);
            return View(dailycandle);
        }

        //
        // GET: /DailyCandles/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DailyCandle dailycandle = db.DailyCandles.Find(id);
            if (dailycandle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarketId = new SelectList(db.Markets, "MarketId", "Symbol", dailycandle.MarketId);
            return View(dailycandle);
        }

        //
        // POST: /DailyCandles/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DailyCandle dailycandle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailycandle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarketId = new SelectList(db.Markets, "MarketId", "Symbol", dailycandle.MarketId);
            return View(dailycandle);
        }

        //
        // GET: /DailyCandles/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DailyCandle dailycandle = db.DailyCandles.Find(id);
            if (dailycandle == null)
            {
                return HttpNotFound();
            }
            return View(dailycandle);
        }

        //
        // POST: /DailyCandles/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyCandle dailycandle = db.DailyCandles.Find(id);
            db.DailyCandles.Remove(dailycandle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
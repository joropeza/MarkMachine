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
    public class MarketsController : Controller
    {
        private marketdbEntities db = new marketdbEntities();

        //
        // GET: /Markets/

        public ActionResult Index()
        {
            return View(db.Markets.ToList());
        }

        //
        // GET: /Markets/Details/5

        public ActionResult Details(int id = 0)
        {
            Market market = db.Markets.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            return View(market);
        }

        //
        // GET: /Markets/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Markets/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Market market)
        {
            if (ModelState.IsValid)
            {
                db.Markets.Add(market);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(market);
        }

        //
        // GET: /Markets/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Market market = db.Markets.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            return View(market);
        }

        //
        // POST: /Markets/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Market market)
        {
            if (ModelState.IsValid)
            {
                db.Entry(market).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(market);
        }

        //
        // GET: /Markets/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Market market = db.Markets.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            return View(market);
        }

        //
        // POST: /Markets/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Market market = db.Markets.Find(id);
            db.Markets.Remove(market);
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
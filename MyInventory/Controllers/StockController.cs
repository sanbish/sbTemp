using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyInventory.Data;

namespace MyInventory.Controllers
{
    public class StockController : Controller
    {
        private InvContext db = new InvContext();

        // GET: Stock
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                var stocks = db.Stocks.Include(s => s.Item_Master).Include(s => s.Location);
                return View(stocks.ToList());
            }
            else
            {
                int Prod_ID;
                    int.TryParse(id, out Prod_ID);
                var stocks = db.Stocks.Include(s => s.Item_Master).Include(s => s.Location).Where(s=>s.Product_ID== Prod_ID);
                return View(stocks.ToList());
            }
        }

        // GET: Stock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            ViewBag.Product_ID = new SelectList(db.Item_Master, "Item_ID", "Item_Name");
            ViewBag.Loc_ID = new SelectList(db.Locations, "Loc_ID", "Location1");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LineItem_ID,Product_ID,Qty,Loc_ID,Price,WIP_Qty,Warranty_Expiry,Item_Expiry")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_ID = new SelectList(db.Item_Master, "Item_ID", "Item_Name", stock.Product_ID);
            ViewBag.Loc_ID = new SelectList(db.Locations, "Loc_ID", "Location1", stock.Loc_ID);
            return View(stock);
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_ID = new SelectList(db.Item_Master, "Item_ID", "Item_Name", stock.Product_ID);
            ViewBag.Loc_ID = new SelectList(db.Locations, "Loc_ID", "Location1", stock.Loc_ID);
            return View(stock);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LineItem_ID,Product_ID,Qty,Loc_ID,Price,WIP_Qty,Warranty_Expiry,Item_Expiry")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_ID = new SelectList(db.Item_Master, "Item_ID", "Item_Name", stock.Product_ID);
            ViewBag.Loc_ID = new SelectList(db.Locations, "Loc_ID", "Location1", stock.Loc_ID);
            return View(stock);
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

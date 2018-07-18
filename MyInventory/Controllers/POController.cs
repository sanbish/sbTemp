using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class POController : Controller
    {
        private InvDBContext db = new InvDBContext();

        // GET: PO
        public ActionResult Index()
        {
            return View(db.PO_Header.ToList());
        }

        // GET: PO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PO_Header pO_Header = db.PO_Header.Find(id);
            if (pO_Header == null)
            {
                return HttpNotFound();
            }
            return View(pO_Header);
        }

        // GET: PO/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult PODetails(int? i)
        {
            ViewBag.i = i;
            ViewBag.Item_ID = new SelectList(db.Item_Master, "Item_ID", "Item_Name");
            return PartialView();
        }


        public ActionResult Receive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PO_Header pO_Header = db.PO_Header.Find(id);
            if (pO_Header == null)
            {
                return HttpNotFound();
            }

            foreach (var detail in pO_Header.PO_Detail)
            {
                Stock stock = db.Stocks.Find(detail.Item_ID);
                if (stock != null)
                {
                    stock.Qty = stock.Qty + detail.Qty;
                    db.Entry(stock).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index","Stocks");
        }

        // POST: PO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PO_ID,PO_Number,Supplier,Date,Amount")] PO_Header pO_Header, List<PO_Detail>  pO_Details)
        public ActionResult Create(PO_Header pO_Header, List<PO_Detail> pO_Details)
        {
            if (ModelState.IsValid)
            {
                db.PO_Header.Add(pO_Header);
                db.SaveChanges();

                int id = db.PO_Header.FirstOrDefault(s => s.PO_Number == pO_Header.PO_Number).PO_ID;
                foreach (var detail in pO_Details)
                {
                    detail.PO_ID = id;
                }
                db.PO_Detail.AddRange(pO_Details);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(pO_Header);
        }

        // GET: PO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PO_Header pO_Header = db.PO_Header.Find(id);
            if (pO_Header == null)
            {
                return HttpNotFound();
            }
            return View(pO_Header);
        }

        // POST: PO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PO_ID,PO_Number,Supplier,Date,Amount")] PO_Header pO_Header)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pO_Header).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pO_Header);
        }

        // GET: PO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PO_Header pO_Header = db.PO_Header.Find(id);
            if (pO_Header == null)
            {
                return HttpNotFound();
            }
            return View(pO_Header);
        }

        // POST: PO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PO_Header pO_Header = db.PO_Header.Find(id);
            db.PO_Header.Remove(pO_Header);
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

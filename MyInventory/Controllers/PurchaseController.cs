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
    public class PurchaseController : Controller
    {
        private InvDBContext db = new InvDBContext();

        // GET: Purchase
        public ActionResult Index()
        {
            return View(db.PO_Header.ToList());
        }

        // GET: Purchase/Details/5
        public ActionResult Details(string id)
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

        // GET: Purchase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PO,Supplier,Date,Amount")] PO_Header pO_Header)
        {
            if (ModelState.IsValid)
            {
                db.PO_Header.Add(pO_Header);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pO_Header);
        }

        // GET: Purchase/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PO,Supplier,Date,Amount")] PO_Header pO_Header)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pO_Header).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pO_Header);
        }

        // GET: Purchase/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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

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
    public class UnitController : Controller
    {
        private InvDBContext db = new InvDBContext();

        // GET: Unit
        public ActionResult Index()
        {
            return View(db.UOMs.ToList());
        }

        // GET: Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UOM uOM = db.UOMs.Find(id);
            if (uOM == null)
            {
                return HttpNotFound();
            }
            return View(uOM);
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Unit_ID,Unit,Description")] UOM uOM)
        {
            if (ModelState.IsValid)
            {
                db.UOMs.Add(uOM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uOM);
        }

        // GET: Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UOM uOM = db.UOMs.Find(id);
            if (uOM == null)
            {
                return HttpNotFound();
            }
            return View(uOM);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Unit_ID,Unit,Description")] UOM uOM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uOM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uOM);
        }

        // GET: Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UOM uOM = db.UOMs.Find(id);
            if (uOM == null)
            {
                return HttpNotFound();
            }
            return View(uOM);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UOM uOM = db.UOMs.Find(id);
            db.UOMs.Remove(uOM);
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

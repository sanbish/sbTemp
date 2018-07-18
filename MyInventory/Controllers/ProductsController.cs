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
    public class ProductsController : Controller
    {
        private InvDBContext db = new InvDBContext();

        // GET: Products
        public ActionResult Index()
        {
            var item_Master = db.Item_Master.Include(i => i.Item_Category).Include(i => i.UOM);
            return View(item_Master.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Master item_Master = db.Item_Master.Find(id);
            if (item_Master == null)
            {
                return HttpNotFound();
            }
            return View(item_Master);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Cat_ID = new SelectList(db.Item_Category, "Category_ID", "Cat_Name");
            ViewBag.Unit = new SelectList(db.UOMs, "Unit_ID", "Unit");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Item_ID,Item_Name,Description,Unit,Cat_ID,Photo")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                db.Item_Master.Add(item_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_ID = new SelectList(db.Item_Category, "Category_ID", "Cat_Name", item_Master.Cat_ID);
            ViewBag.Unit = new SelectList(db.UOMs, "Unit_ID", "Unit", item_Master.Unit);
            return View(item_Master);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Master item_Master = db.Item_Master.Find(id);
            if (item_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_ID = new SelectList(db.Item_Category, "Category_ID", "Cat_Name", item_Master.Cat_ID);
            ViewBag.Unit = new SelectList(db.UOMs, "Unit_ID", "Unit", item_Master.Unit);
            return View(item_Master);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Item_ID,Item_Name,Description,Unit,Cat_ID,Photo")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_ID = new SelectList(db.Item_Category, "Category_ID", "Cat_Name", item_Master.Cat_ID);
            ViewBag.Unit = new SelectList(db.UOMs, "Unit_ID", "Unit", item_Master.Unit);
            return View(item_Master);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Master item_Master = db.Item_Master.Find(id);
            if (item_Master == null)
            {
                return HttpNotFound();
            }
            return View(item_Master);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item_Master item_Master = db.Item_Master.Find(id);
            db.Item_Master.Remove(item_Master);
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

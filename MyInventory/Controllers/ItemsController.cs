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
    public class ItemsController : Controller
    {
        private InvDBContext db = new InvDBContext();

        // GET: Items
        public ActionResult Index()
        {
            var item_Master = db.Item_Master.Include(i => i.Item_Category).Include(i => i.UOM);
            return View(item_Master.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
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

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.C_Code = new SelectList(db.Item_Category, "C_Code", "Category");
            ViewBag.Unit = new SelectList(db.UOMs, "Unit", "Description");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Item_ID,Item,Item_Description,Unit,C_Code")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                db.Item_Master.Add(item_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_Code = new SelectList(db.Item_Category, "C_Code", "Category", item_Master.C_Code);
            ViewBag.Unit = new SelectList(db.UOMs, "Unit", "Description", item_Master.Unit);
            return View(item_Master);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.C_Code = new SelectList(db.Item_Category, "C_Code", "Category", item_Master.C_Code);
            ViewBag.Unit = new SelectList(db.UOMs, "Unit", "Description", item_Master.Unit);
            return View(item_Master);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Item_ID,Item,Item_Description,Unit,C_Code")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_Code = new SelectList(db.Item_Category, "C_Code", "Category", item_Master.C_Code);
            ViewBag.Unit = new SelectList(db.UOMs, "Unit", "Description", item_Master.Unit);
            return View(item_Master);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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

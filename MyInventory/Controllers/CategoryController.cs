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
    public class CategoryController : Controller
    {
        private InvDBContext db = new InvDBContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Item_Category.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Category item_Category = db.Item_Category.Find(id);
            if (item_Category == null)
            {
                return HttpNotFound();
            }
            return View(item_Category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_ID,Cat_Name,Description,Aprroval_Needed")] Item_Category item_Category)
        {
            if (ModelState.IsValid)
            {
                db.Item_Category.Add(item_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item_Category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Category item_Category = db.Item_Category.Find(id);
            if (item_Category == null)
            {
                return HttpNotFound();
            }
            return View(item_Category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_ID,Cat_Name,Description,Aprroval_Needed")] Item_Category item_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item_Category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Category item_Category = db.Item_Category.Find(id);
            if (item_Category == null)
            {
                return HttpNotFound();
            }
            return View(item_Category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item_Category item_Category = db.Item_Category.Find(id);
            db.Item_Category.Remove(item_Category);
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

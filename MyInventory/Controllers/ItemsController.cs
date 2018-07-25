using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class ItemsController : Controller
    {
        private const string FolderPath = "/Assets/img/Uploads/";
        private InvDBContext db = new InvDBContext();

        // GET: Items
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

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.Cat_ID = new SelectList(db.Item_Category, "Category_ID", "Cat_Name");
            ViewBag.Unit = new SelectList(db.UOMs, "Unit_ID", "Unit");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file,[Bind(Include = "Item_ID,Item_Name,Description,Unit,Cat_ID,Photo")] Item_Master item_Master )
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.InputStream.Read(new byte[file.ContentLength], 0, file.ContentLength);
                    var filename = "Prod_" + DateTime.Now.Ticks.ToString() + ".jpg";
                    var filePath = Server.MapPath(FolderPath);

                    string savedFileName = Path.Combine(filePath, filename);
                    file.SaveAs(savedFileName);
                    item_Master.Photo = FolderPath + filename;
                }

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

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, [Bind(Include = "Item_ID,Item_Name,Description,Unit,Cat_ID,Photo")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.InputStream.Read(new byte[file.ContentLength], 0, file.ContentLength);
                    var filename = "Prod_" + DateTime.Now.Ticks.ToString() + ".jpg";
                    if (!string.IsNullOrEmpty(item_Master.Photo))
                    {
                        filename = item_Master.Photo;
                    }
                    var filePath = Server.MapPath(FolderPath);

                    string savedFileName = Path.Combine(filePath, filename);
                    file.SaveAs(savedFileName);
                    item_Master.Photo = savedFileName;
                }


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

        // POST: Items/Delete/5
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

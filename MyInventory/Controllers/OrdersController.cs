using Microsoft.AspNet.Identity;
using MyInventory.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyInventory.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private InvContext db = new InvContext();

        // GET: Orders
        public ActionResult Index()
        {
            
            return View(db.Order_Master.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Master order_Master = db.Order_Master.Find(id);
            if (order_Master == null)
            {
                return HttpNotFound();
            }
            return View(order_Master);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");

            string ordNo = "Ord00000001";
            int? max = (from o in db.Order_Master
                        select (int?)o.Id).Max();
            if (max == 0 || max == null)
            {
                max = 1;
                ordNo = max.ToString().PadLeft(5, '0');
                ordNo = "ORD" + ordNo;
            }
            else
            {
                int? nextmax = max + 1;
                ordNo = nextmax.ToString().PadLeft(5, '0');
                ordNo = "ORD" + ordNo; ;
            }
            ViewBag.OrderNo = ordNo;
            return View();
        }

        public ActionResult OrderDetails(int? i)
        {
            ViewBag.i = i;
            ViewBag.StockId = new SelectList(db.vwStocks.Where(p => p.Loc_ID == 1), "LineItem_ID", "ProductName");
            //ViewBag.StockId = new SelectList(), "LineItem_ID", "ProductName");
            return PartialView();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Comments,CreatedBy,CreatedDate,CustomerId,Discount,UpdatedBy,UpdatedDate")] Order_Master order_Master, List<Order_Details> order_Details)
        {
            if (ModelState.IsValid)
            {
                string UserId = System.Web.HttpContext.Current.User.Identity.Name;
                order_Master.CreatedBy = UserId;
                string ordNo = "Ord00000001";
                int? max = (from o in db.Order_Master
                            select (int?)o.Id).Max();
                if (max == 0 || max == null)
                {
                    max = 1;
                    ordNo = max.ToString().PadLeft(5, '0');
                    ordNo = "ORD" + ordNo;
                }
                else
                {
                    int? nextmax = max + 1;
                    ordNo = nextmax.ToString().PadLeft(5, '0');
                    ordNo = "ORD" + ordNo; ;
                }

                order_Master.Order_No = ordNo;
                order_Master.CreatedDate = DateTime.Now;

                db.Order_Master.Add(order_Master);
                db.SaveChanges();

                int id = db.Order_Master.FirstOrDefault(s => s.Order_No == order_Master.Order_No ).Id;
                int OrderLine = 1;
                foreach (var detail in order_Details)
                {
                    detail.OrderId = id;
                    detail.Order_Line = OrderLine;
                    detail.CreatedBy = UserId;
                    detail.CreatedDate= DateTime.Now;
                    OrderLine++;
                }
                db.Order_Details.AddRange(order_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order_Master.UserId);
            return View(order_Master);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Master order_Master = db.Order_Master.Find(id);
            if (order_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order_Master.UserId);
            return View(order_Master);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Comments,CreatedBy,CreatedDate,CustomerId,Discount,UpdatedBy,UpdatedDate")] Order_Master order_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order_Master.UserId);
            return View(order_Master);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Master order_Master = db.Order_Master.Find(id);
            if (order_Master == null)
            {
                return HttpNotFound();
            }
            return View(order_Master);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Master order_Master = db.Order_Master.Find(id);
            db.Order_Master.Remove(order_Master);
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

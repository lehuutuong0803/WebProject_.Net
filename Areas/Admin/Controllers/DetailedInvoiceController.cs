using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Areas.Admin.Controllers
{
    public class DetailedInvoiceController : BaseController
    {
        private WebProjectEntities8 db = new WebProjectEntities8();

        // GET: Admin/DetailedInvoice
        public ActionResult Index()
        {
            var detailedInvoices = db.DetailedInvoices.Include(d => d.Food).Include(d => d.Invoice);
            return View(detailedInvoices.ToList());
        }

        // GET: Admin/DetailedInvoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detailedInvoice = db.DetailedInvoices.Where(s => s.IDInvoice == id).ToList();
            if (detailedInvoice == null)
            {
                return HttpNotFound();
            }
            return View(detailedInvoice);
        }

        // GET: Admin/DetailedInvoice/Create
        public ActionResult Create()
        {
            ViewBag.IDFood = new SelectList(db.Foods, "ID", "FoodName");
            ViewBag.IDInvoice = new SelectList(db.Invoices, "ID", "Note");
            return View();
        }

        // POST: Admin/DetailedInvoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDInvoice,IDFood,IntoMoney")] DetailedInvoice detailedInvoice)
        {
            if (ModelState.IsValid)
            {
                db.DetailedInvoices.Add(detailedInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDFood = new SelectList(db.Foods, "ID", "FoodName", detailedInvoice.IDFood);
            ViewBag.IDInvoice = new SelectList(db.Invoices, "ID", "Note", detailedInvoice.IDInvoice);
            return View(detailedInvoice);
        }

        // GET: Admin/DetailedInvoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailedInvoice detailedInvoice = db.DetailedInvoices.Find(id);
            if (detailedInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDFood = new SelectList(db.Foods, "ID", "FoodName", detailedInvoice.IDFood);
            ViewBag.IDInvoice = new SelectList(db.Invoices, "ID", "Note", detailedInvoice.IDInvoice);
            return View(detailedInvoice);
        }

        // POST: Admin/DetailedInvoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDInvoice,IDFood,IntoMoney")] DetailedInvoice detailedInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailedInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDFood = new SelectList(db.Foods, "ID", "FoodName", detailedInvoice.IDFood);
            ViewBag.IDInvoice = new SelectList(db.Invoices, "ID", "Note", detailedInvoice.IDInvoice);
            return View(detailedInvoice);
        }

        // GET: Admin/DetailedInvoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailedInvoice detailedInvoice = db.DetailedInvoices.Find(id);
            if (detailedInvoice == null)
            {
                return HttpNotFound();
            }
            return View(detailedInvoice);
        }

        // POST: Admin/DetailedInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailedInvoice detailedInvoice = db.DetailedInvoices.Find(id);
            db.DetailedInvoices.Remove(detailedInvoice);
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

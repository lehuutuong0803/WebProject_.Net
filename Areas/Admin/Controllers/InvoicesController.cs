using ClosedXML.Excel;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebProject.Models;

namespace WebProject.Areas.Admin.Controllers
{
    public class InvoicesController : BaseController
    {
        private WebProjectEntities8 db = new WebProjectEntities8();

        // GET: Admin/Invoices
        public ActionResult Index(string username, int page = 1, int pageSize = 9)
        {
            
            IQueryable<Invoice> model = db.Invoices;
            if (!string.IsNullOrEmpty(username))
            {
                int id = int.Parse(username);
                model = model.Where(x => x.IDCustomer==id).OrderBy(x => x.ID);
            }

            return View(model.OrderBy(x => x.ID).ToPagedList(page, pageSize));
        }

        // GET: Admin/Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Admin/Invoices/Create
        public ActionResult Create()
        {
            ViewBag.IDCustomer = new SelectList(db.Users, "ID", "UserName");
            return View();
        }

        // POST: Admin/Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCustomer,TotalPrice,Note,Date,Status")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCustomer = new SelectList(db.Users, "ID", "UserName", invoice.IDCustomer);
            return View(invoice);
        }

        // GET: Admin/Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCustomer = new SelectList(db.Users, "ID", "UserName", invoice.IDCustomer);
            return View(invoice);
        }

        // POST: Admin/Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCustomer,TotalPrice,Note,Date,Status")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCustomer = new SelectList(db.Users, "ID", "UserName", invoice.IDCustomer);
            return View(invoice);
        }

        // GET: Admin/Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Admin/Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
       public ActionResult ExportExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var list = db.Invoices.OrderBy(s => s.ID).ToList();
                int totalInvoiceHT =0 , totalInvoiceCHT = 0;
                double totalPriceHT =0, totalPriceCHT = 0, total = 0;


                var worksheet = workbook.Worksheets.Add("Students");
                var currenRow = 1;

                
                foreach(var item in list)
                {
                    if(item.Status == 0)
                    {
                        totalInvoiceCHT++;
                        totalPriceCHT += (double)item.TotalPrice;
                    }
                    else
                    {
                        totalInvoiceHT++;
                        totalPriceHT += (double)item.TotalPrice;
                    }
                    total += (double)item.TotalPrice;
                }    

                #region Header
                worksheet.Cell(currenRow, 1).Value = "Mã Hóa Đơn";
                worksheet.Cell(currenRow, 2).Value = "Mã Khách Hàng";
                worksheet.Cell(currenRow, 3).Value = "Ghi Chú";
                worksheet.Cell(currenRow, 4).Value = "Ngày lập";
                worksheet.Cell(currenRow, 5).Value = "Trạng thái";
                worksheet.Cell(currenRow, 6).Value = "Tổng giá";
                worksheet.Cell(currenRow, 7).Value = "Tổng hóa đơn HT";
                worksheet.Cell(currenRow, 8).Value = "Tổng hóa đơn chưa HT";
                worksheet.Cell(currenRow, 9).Value = "Tổng tiền hóa đơn HT";
                worksheet.Cell(currenRow, 10).Value = "Tổng tiền hóa đơn CHT";
                worksheet.Cell(currenRow, 11).Value = "Tổng tiền tất cả hóa đơn ";
                #endregion
                #region Body
                foreach (var item in list)
                {
                    currenRow++;
                    worksheet.Cell(currenRow, 1).Value = item.ID;
                    worksheet.Cell(currenRow, 2).Value = item.IDCustomer;
                    worksheet.Cell(currenRow, 3).Value = item.Note;
                    worksheet.Cell(currenRow, 4).Value = item.Date.ToString();
                    worksheet.Cell(currenRow, 5).Value = item.Status;
                    worksheet.Cell(currenRow, 6).Value = item.TotalPrice;
                }
                worksheet.Cell(2, 7).Value = totalInvoiceHT;
                worksheet.Cell(2, 8).Value = totalInvoiceCHT;
                worksheet.Cell(2, 9).Value = totalPriceHT;
                worksheet.Cell(2, 10).Value = totalPriceCHT;
                worksheet.Cell(2, 11).Value = total;
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Invoice.xlsx");
                }    
            }
        }
        public ActionResult Tracking(FormCollection s)
        {
            int id = int.Parse(s["idIV"]);
            var search = db.Invoices.Where(x => x.ID == id).FirstOrDefault();
            if(search.Status == 0 )
            {
                search.Status = 1;
                db.Entry(search).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

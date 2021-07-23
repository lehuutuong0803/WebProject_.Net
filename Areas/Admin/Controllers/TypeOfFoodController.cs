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
    public class TypeOfFoodController : BaseController
    {
        private WebProjectEntities db = new WebProjectEntities();

        // GET: Admin/TypeOfFood
        public ActionResult Index()
        {
            return View(db.TypeOfFoods.ToList());
        }

        // GET: Admin/TypeOfFood/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfFood typeOfFood = db.TypeOfFoods.Find(id);
            if (typeOfFood == null)
            {
                return HttpNotFound();
            }
            return View(typeOfFood);
        }

        // GET: Admin/TypeOfFood/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TypeOfFood/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TypeName")] TypeOfFood typeOfFood)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfFoods.Add(typeOfFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeOfFood);
        }

        // GET: Admin/TypeOfFood/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfFood typeOfFood = db.TypeOfFoods.Find(id);
            if (typeOfFood == null)
            {
                return HttpNotFound();
            }
            return View(typeOfFood);
        }

        // POST: Admin/TypeOfFood/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TypeName")] TypeOfFood typeOfFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeOfFood);
        }

        // GET: Admin/TypeOfFood/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfFood typeOfFood = db.TypeOfFoods.Find(id);
            if (typeOfFood == null)
            {
                return HttpNotFound();
            }
            return View(typeOfFood);
        }

        // POST: Admin/TypeOfFood/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeOfFood typeOfFood = db.TypeOfFoods.Find(id);
            db.TypeOfFoods.Remove(typeOfFood);
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

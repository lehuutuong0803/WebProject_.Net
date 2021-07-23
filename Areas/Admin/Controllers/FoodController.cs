using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Areas.Admin.Controllers
{
    public class FoodController : BaseController
    {
        private WebProjectEntities db = new WebProjectEntities();

        // GET: Admin/Food
        public ActionResult Index(string foodname, int page = 1, int pageSize = 9)
        {
            IQueryable<Food> model = db.Foods.Include(f => f.TypeOfFood);
            if (!string.IsNullOrEmpty(foodname))
            {
                model = model.Where(x => x.FoodName.Contains(foodname)).OrderBy(x => x.ID);
            }

            return View(model.OrderBy(x => x.ID).ToPagedList(page, pageSize));
        }

        // GET: Admin/Food/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Admin/Food/Create
        public ActionResult Create()
        {
            ViewBag.IDType = new SelectList(db.TypeOfFoods, "ID", "TypeName");
            return View();
        }

        // POST: Admin/Food/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FoodName,Price,IDType,Status,Description,Image")] Food food,HttpPostedFileBase image)
        {
            
            if (ModelState.IsValid)
            {
                var test = db.Foods.ToList();
                int id;
                if (test.Count==0)
                {
                    id = 0;
                }
                else
                {
                    id = int.Parse(db.Foods.ToList().Last().ID.ToString());
                }
                 
                string fileName = "";
                int id1 = id + 1;
                int index = image.FileName.IndexOf('.');
                fileName = "food" + id1.ToString()+ "." + image.FileName.Substring(index+1);
                string path = Path.Combine(Server.MapPath("~/images"), fileName);
                image.SaveAs(path);

               
                food.Image = fileName;
                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDType = new SelectList(db.TypeOfFoods, "ID", "TypeName", food.IDType);
            return View(food);
        }

        // GET: Admin/Food/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDType = new SelectList(db.TypeOfFoods, "ID", "TypeName", food.IDType);
            return View(food);
        }

        // POST: Admin/Food/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FoodName,Price,IDType,Status,Description,Image")] Food food, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                int id = food.ID;
                string fileName = "";
                int id1 = id + 1;
                DateTime date = DateTime.Today;
                int index = image.FileName.IndexOf('.');
                fileName = "foodEdit" + id1.ToString()+date.ToString("HH-mm-ss") + "." + image.FileName.Substring(index + 1);
                string path = Path.Combine(Server.MapPath("~/images"), fileName);
                image.SaveAs(path);


                food.Image = fileName;

                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDType = new SelectList(db.TypeOfFoods, "ID", "TypeName", food.IDType);
            return View(food);
        }

        // GET: Admin/Food/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Admin/Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
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

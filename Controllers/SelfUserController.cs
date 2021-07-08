using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class SelfUserController : Controller
    {
        private WebProjectEntities5 db = new WebProjectEntities5();

        // GET: SelfUser
        public ActionResult Index()
        {
            return RedirectToAction("../Home/Index");
        }

        // GET: SelfUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: SelfUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SelfUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,PassWord,Name,Address,SDT,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: SelfUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: SelfUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,PassWord,Name,Address,SDT,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: SelfUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: SelfUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        public ActionResult ChangePW()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePW(FormCollection s)
        {
            string chuoi = "";
            string OldPW = s["OldPW"];
            string NewPW1 = s["NewPW1"];
            string NewPW2 = s["NewPW2"];
            int id = (int)Session["UserIDCTM"];
            User user = db.Users.Find(id);
            if (OldPW == user.PassWord)
            {
                if(NewPW1 == NewPW2)
                {
                    user.PassWord = NewPW1;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    chuoi = "Cập nhật mật khẩu thành công!";
                    ViewBag.Update = chuoi;
                    return View("ChangePW");
                }
                else
                {
                    chuoi = "Mật khẩu mới không khớp nhau. Vui Lòng thử lại!!!";
                    ViewBag.Update = chuoi;
                    return View("ChangePW");
                }
              
            }
            else
            {

                chuoi = "Mật khẩu hiện tại không chính xác. Vui Lòng thử lại!!!";
                ViewBag.Update = chuoi;
                return View("ChangePW");
            }
        }
    }
}

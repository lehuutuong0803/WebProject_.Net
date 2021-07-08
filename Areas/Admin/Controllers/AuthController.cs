using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        WebProjectEntities5 dbcontext = new WebProjectEntities5();

        // GET: Admin/Auth  
        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection s)
        {
            String username = s["username"];
            String password = s["password"];
            User user = dbcontext.Users.Where(m => m.UserName == username && m.PassWord == password)
                .FirstOrDefault();

            int y = dbcontext.Users.Where(m => true).Count();
            Session["AmountOfUser"] = y;
            y= dbcontext.Foods.Where(m => true).Count();
            Session["AmountOfFood"] = y;
            y = dbcontext.TypeOfFoods.Where(m => true).Count();
            Session["AmountOfTypeFood"] = y;
            y = dbcontext.Comments.Where(m => true).Count();
            Session["AmountOfComment"] = y;
            y = dbcontext.Invoices.Where(m => true).Count();
            Session["AmountOfInvoice"] = y;



            string error = "";
            if (user != null)
            {
                Session["UserAdmin"] = user.UserName;
                Session["UserID"] = user.ID;
                Session["UserName"] = user.Name;
                Response.Redirect("~/Admin");
                
            }
            else
            {
                error = "Tài khoản hoặc mật khuẩu không chính xác. Vui lòng thử lại! ";
                ViewBag.Error = error;
            }
              
           
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserID"] = "";
            Session["UserName"] = "";
            Response.Redirect("~/Admin/Auth/Login");
            return null;
        }
    }
}
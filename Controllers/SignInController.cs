using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
using Xunit;

namespace WebProject.Controllers
{
    public class SignInController : Controller
    {
        WebProjectEntities8 db = new WebProjectEntities8();
        // GET: SignIn
        public ActionResult Login(  )
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String tentaikhoan, String matkhau, String email, String hoten, String diachi, String sdt)
        {
            User user = new User();
            user.UserName = tentaikhoan;
            user.PassWord = matkhau;
            user.Email = email;
            user.Name = hoten;
            user.Address = diachi;
            user.SDT = sdt;

            string inform = "";
            var search = db.Users.Where(m => m.UserName == user.UserName).FirstOrDefault();
            if (search == null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                inform = "Đăng ký thành công. Hãy đăng nhập!";
                ViewBag.Inform = inform;
            }
            else
            {
                inform = "Tên tài khoản đã tồn tại. Vui lòng thử lại! ";
                ViewBag.Inform = inform;
            }
            return View();
        }
          [HttpPost,ActionName("Login1")]
          public ActionResult Login(String tentaikhoan, String matkhau)
          {
            string inform = "";
            var search = db.Users.Where(m => m.UserName == tentaikhoan && m.PassWord == matkhau).FirstOrDefault();
            if (search != null)
            {
                Session["UserCTM"] = search.UserName;
                Session["UserIDCTM"] = search.ID;
                Session["UserCTMName"] = search.Name;
                Session["UserCTMAddress"] = search.Address;
                Session["UserCTMPhoneNumber"] = search.SDT;
                Response.Redirect("~/Home/Index");
            }
            else
            {
                 inform = "Tài khoản hoặc mật khẩu không đúng.Vui lòng thử lại!";
                ViewBag.Inform = inform;
                //Response.Redirect("~/SignIn/Login");
                return View("Login");
            }
            Session["Status"] = "1";
            Session["CustomerName"] = search.Name; 

            return View();
          }

        public ActionResult Logout()
        {
            Session["Status"] = null;
            Session["CustomerName"] = null;
            Session["UserCTM"] = null;
            Session["UserIDCTM"] = null;
            Session["UserCTMPhoneNumber"] = null;
            Response.Redirect("~/Home/Index");
            return null;
        }

        [HttpPost,ActionName("verifyGoogle")]
        public async System.Threading.Tasks.Task<ActionResult> verifyGoogleAsync(FormCollection values)
        {
            var exa = values["idtoken"];
            
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(exa);


            Session["exaIdToken"] = validPayload.Audience;
            if ( validPayload == null
                || validPayload.Audience.ToString() != "841081615376-3hbsvqkqpj1u4p943gpj93v0ki62tmjo.apps.googleusercontent.com")
                Response.Redirect("~/Home/Index");

            else
            {
                User user = new User();
                user.UserName = validPayload.Subject;
                user.PassWord = validPayload.Subject;
                user.Email = validPayload.Email;
                user.Name = validPayload.Name;
                user.Address = "";
                user.SDT = "";
                var search = db.Users.Where(m => m.UserName == user.UserName).FirstOrDefault();
                if (search == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }

            Login(validPayload.Subject, validPayload.Subject);
            Response.Redirect("~/Home/Index");

            return null;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if(Session["UserAdmin"].Equals(""))
            {
                Response.Redirect("~/Admin/Login");
            }
            return View();
        }
    }
}
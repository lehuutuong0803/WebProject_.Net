using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            //Admin
            Session["UserAdmin"] = "";
            Session["UserID"] = "";
            Session["UserName"] = "";

            //Customer
            Session["UserCTM"] = "";
            Session["UserIDCTM"] = "";
            Session["UserCTMName"] = "";
            Session["UserCTMAddress"] = "";
            Session["UserCTMPhoneNumber"] = "";
        }
      

    }
}

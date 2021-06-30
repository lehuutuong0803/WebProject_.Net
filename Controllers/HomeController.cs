using System.Web.Mvc;
using WebProject.Models;
using System.Linq;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        WebProjectEntitiesDB dbcontext = new WebProjectEntitiesDB();

        public ActionResult Index()
        {
            var list = dbcontext.Foods.ToList();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Company()
        {
            return View();
        }
        public ActionResult Furniture()
        {
            return View();
        }
    }
}
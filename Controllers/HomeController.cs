using System.Web.Mvc;
using WebProject.Models;
using System.Linq;
using System.Net;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        WebProjectEntities5 dbcontext = new WebProjectEntities5();

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
        public ActionResult Menu()
        {
            var list = dbcontext.Foods.ToList();
            return View(list);
        }
        public ActionResult Food_Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = dbcontext.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        public ActionResult Stuff()
        {
            var list = dbcontext.Stuffs.ToList();

            return View(list);
        }

        public ActionResult Reservation()
        {
            var userNow = dbcontext.Users.Find(Session["UserIDCTM"]);
            ViewBag.userNow = userNow;
            return View(userNow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation([Bind(Include = "Date,Time,AmoutPerson,NameTheBooker,EmailTheBooker,NumberTheBooker,Id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                dbcontext.Reservations.Add(reservation);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }




    }
}
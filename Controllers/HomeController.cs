using System.Web.Mvc;
using WebProject.Models;
using System.Linq;
using System.Net;
using PagedList;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        WebProjectEntities dbcontext = new WebProjectEntities();

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
        public ActionResult Menu(string foodname,int id =0,int page = 1,int pageSize = 9 )
        {
            IQueryable<Food> model = dbcontext.Foods;
            if(!string.IsNullOrEmpty(foodname))
            {
                model = (IQueryable<Food>)model.Where(x => x.FoodName.Contains(foodname));
            }
            if(id ==0)
            {    
            }
            else
            {
                model = model.Where(x => x.IDType == id);
            }
           
            return View(model.OrderBy(x => x.ID).ToPagedList(page, pageSize));
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
        public ActionResult History(int page = 1, int pageSize = 5)
        {
            int id;
            if (System.Web.HttpContext.Current.Session["UserIDCTM"].Equals(""))
            {
                return Content("Bạn chưa đăng nhập hoặc xuất hiện lỗi bất ngờ. Vui lòng thử lại!");
            }
            else
            {
                id = (int)Session["UserIDCTM"];
                IQueryable<Invoice> history = dbcontext.Invoices.Where(s => s.IDCustomer == id);
                    return View(history.OrderBy(x => x.ID).ToPagedList(page, pageSize));
            }       
        }
    }
}
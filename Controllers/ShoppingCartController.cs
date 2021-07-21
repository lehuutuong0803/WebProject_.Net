using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        WebProjectEntities5 db = new WebProjectEntities5();


        public ActionResult Index()
        {

            return View();
        }
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null|| Session["Cart"] ==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var food = db.Foods.SingleOrDefault(s => s.ID == id);
            if (food != null)
            {
                GetCart().Add(food);
            }

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult ShowToCart()
        {
            if(Session["Cart"] ==null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                Cart cart = Session["Cart"] as Cart;
                return  View(cart);
            }
        }
        public ActionResult Update_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id =int.Parse(form["ID_Food"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity(id, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)
            {
                t_item = cart.Total_Quantity();
            }
            ViewBag.InforCart = t_item;
            return PartialView("BagCart");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Order(FormCollection s )
        {
            try
            {
                Invoice invoice = new Invoice();
                Cart cart = Session["Cart"] as Cart;
                invoice.IDCustomer = (int?)Session["UserIDCTM"];
                invoice.Note = s["Note"];
                invoice.TotalPrice = cart.Total();
                invoice.Date = DateTime.Now;
                invoice.Status = 0;
                db.Invoices.Add(invoice);
                foreach (var item in cart.Items)
                {
                    DetailedInvoice detailedInvoice = new DetailedInvoice();
                    detailedInvoice.IDInvoice = invoice.ID;
                    detailedInvoice.IDFood = item.food_Cart.ID;
                    detailedInvoice.IntoMoney = item.quantity_Cart * item.food_Cart.Price;
                    detailedInvoice.Quantity = item.quantity_Cart;
                    db.DetailedInvoices.Add(detailedInvoice);
                }
                db.SaveChanges();
                return RedirectToAction("Success", "ShoppingCart");
            }
            catch
            {
                return Content("Bạn chưa đăng nhập hoặc xuất hiện lỗi bất ngờ. Vui lòng thử lại!");
            }
           
        }
      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detailedInvoice = db.DetailedInvoices.Where(s => s.IDInvoice == id).ToList();
            if (detailedInvoice == null)
            {
                return HttpNotFound();
            }
            return View(detailedInvoice);
        }

    }
}
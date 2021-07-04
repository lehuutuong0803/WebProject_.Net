using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        WebProjectEntitiesDB db = new WebProjectEntitiesDB();
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
                return RedirectToAction("ShowToCart", "ShoppingCart");
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
    }
}
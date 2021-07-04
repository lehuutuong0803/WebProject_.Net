using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class CartItem
    {
        public Food food_Cart { get; set; }
        public int quantity_Cart { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(Food food, int quantity  = 1)
        {
            var item = items.FirstOrDefault(s => s.food_Cart.ID == food.ID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    food_Cart = food,
                    quantity_Cart = quantity
                });
            }
            else
            {
                item.quantity_Cart += quantity;
            }
        }
        public void Update_Quantity(int id,int quantity)
        {
            var item = items.Find(s => s.food_Cart.ID == id);
                if(item != null)
            {
                item.quantity_Cart = quantity;
            }
        }
        public double Total()
        {
            var total = items.Sum(s => s.food_Cart.Price * s.quantity_Cart);
            return Convert.ToDouble(total);
        }
        public void Remove(int id)
        {
            items.RemoveAll(s => s.food_Cart.ID == id);
        }
        public int Total_Quantity()
        {
            return items.Sum(s => s.quantity_Cart);
        }
    }
}
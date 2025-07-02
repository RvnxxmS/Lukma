using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lukma.Models
{
    public class CartItem
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
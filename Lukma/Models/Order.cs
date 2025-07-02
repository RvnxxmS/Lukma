using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lukma.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Preparing,
        Delivered,
        Cancelled
    }
}
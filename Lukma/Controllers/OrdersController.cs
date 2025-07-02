using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Lukma.Models;

[Authorize]
public class OrdersController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [Authorize(Roles = "Customer")]
    public ActionResult PlaceOrder()
    {
        var cart = Session["Cart"] as Cart;
        if (cart == null || !cart.Items.Any()) return RedirectToAction("Index", "Restaurants");
        var order = new Order
        {
            CustomerId = User.Identity.GetUserId(),
            Status = OrderStatus.Pending,
            OrderItems = cart.Items.Select(c => new OrderItem { MenuItemId = c.MenuItemId, Quantity = c.Quantity }).ToList()
        };
        db.Orders.Add(order);
        db.SaveChanges();
        Session["Cart"] = null;
        return RedirectToAction("MyOrders");
    }

    [Authorize(Roles = "Customer")]
    public ActionResult MyOrders()
    {
        var userId = User.Identity.GetUserId();
        var orders = db.Orders.Where(o => o.CustomerId == userId).ToList();
        return View(orders);
    }

    [Authorize(Roles = "Customer")]
    public ActionResult CancelOrder(int id)
    {
        var order = db.Orders.Find(id);
        if (order != null && order.CustomerId == User.Identity.GetUserId() && order.Status == OrderStatus.Pending)
        {
            order.Status = OrderStatus.Cancelled;
            db.SaveChanges();
        }
        return RedirectToAction("MyOrders");
    }

    [Authorize(Roles = "Admin")]
    public ActionResult AllOrders()
    {
        var orders = db.Orders.ToList();
        return View(orders);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult UpdateStatus(int id, OrderStatus status)
    {
        var order = db.Orders.Find(id);
        if (order != null)
        {
            order.Status = status;
            db.SaveChanges();
        }
        return RedirectToAction("AllOrders");
    }
}
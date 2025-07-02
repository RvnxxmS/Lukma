using System.Linq;
using System.Web.Mvc;
using Lukma.Models;

public class CartController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();
    private const string CartSessionKey = "Cart";

    [Authorize(Roles = "Customer")]
    public ActionResult AddToCart(int menuItemId, int quantity = 1)
    {
        var cart = GetCart();
        var menuItem = db.MenuItems.Find(menuItemId);
        if (menuItem != null)
        {
            var cartItem = cart.Items.FirstOrDefault(c => c.MenuItemId == menuItemId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    MenuItemId = menuItem.Id,
                    MenuItemName = menuItem.Name,
                    Price = menuItem.Price,
                    Quantity = quantity
                });
            }
            Session[CartSessionKey] = cart;
        }
        return RedirectToAction("Index", "Restaurants");
    }

    [Authorize(Roles = "Customer")]
    public ActionResult ViewCart()
    {
        var cart = GetCart();
        return View(cart);
    }

    private Cart GetCart()
    {
        var cart = Session[CartSessionKey] as Cart ?? new Cart();
        return cart;
    }
}
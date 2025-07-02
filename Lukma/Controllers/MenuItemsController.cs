using System.Linq;
using System.Web.Mvc;
using Lukma.Models;

public class MenuItemsController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index(int? restaurantId)
    {
        if (!restaurantId.HasValue)
        {
            return RedirectToAction("Index", "Restaurants"); // Redirect to restaurants if no ID
        }
        var menuItems = db.MenuItems.Where(m => m.RestaurantId == restaurantId.Value).ToList();
        ViewBag.RestaurantId = restaurantId.Value;
        return View(menuItems);
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create(int? restaurantId)
    {
        if (!restaurantId.HasValue)
        {
            return RedirectToAction("Index", "Restaurants");
        }
        ViewBag.RestaurantId = restaurantId.Value;
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult Create(int? restaurantId, MenuItem menuItem)
    {
        if (!restaurantId.HasValue)
        {
            return RedirectToAction("Index", "Restaurants");
        }
        if (ModelState.IsValid)
        {
            menuItem.RestaurantId = restaurantId.Value;
            db.MenuItems.Add(menuItem);
            db.SaveChanges();
            return RedirectToAction("Index", new { restaurantId = restaurantId.Value });
        }
        ViewBag.RestaurantId = restaurantId.Value;
        return View(menuItem);
    }
}
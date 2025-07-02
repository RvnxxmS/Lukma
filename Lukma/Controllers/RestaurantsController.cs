using System.Linq;
using System.Web.Mvc;
using Lukma.Models;

public class RestaurantsController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
        return View(db.Restaurants.ToList());
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult Create(Restaurant restaurant)
    {
        if (ModelState.IsValid)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(restaurant);
    }
}
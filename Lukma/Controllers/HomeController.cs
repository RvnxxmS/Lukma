using System.Web.Mvc;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        ViewBag.Description = "Welcome to Lukma, your adorable food delivery app! Browse restaurants, order your favorite meals, and enjoy a delightful experience. Created by Raneem Abdelsamea, Saly Hussein, and Tamar Lamparadze.";
        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Contact the Lukma team:\n- Raneem Abdelsamea - raneem@example.com (Admin)\n- Saly Hussein - saly@example.com (Admin)\n- Tamar Lamparadze - tamar@example.com (Admin)\nFor admin access, use the above emails with the password: Contact admins for password.";
        return View();
    }
}
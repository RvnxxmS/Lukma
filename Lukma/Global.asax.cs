using Lukma.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lukma
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SeedRolesAndUsers();
            SeedRestaurants();
        }

        private void SeedRolesAndUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Create roles
                string[] roles = { "Admin", "Customer" };
                foreach (var role in roles)
                {
                    if (!roleManager.RoleExists(role))
                    {
                        roleManager.Create(new IdentityRole(role));
                    }
                }

                // Create admin users
                var admins = new[]
                {
                    new { Email = "raneem@example.com", Name = "Raneem Abdelsamea" },
                    new { Email = "saly@example.com", Name = "Saly Hussein" },
                    new { Email = "tamar@example.com", Name = "Tamar Lamparadze" }
                };

                foreach (var admin in admins)
                {
                    if (userManager.FindByEmail(admin.Email) == null)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = admin.Email,
                            Email = admin.Email
                        };
                        var result = userManager.Create(user, "Password123!");
                        if (result.Succeeded)
                        {
                            userManager.AddToRole(user.Id, "Admin");
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private void SeedRestaurants()
        {
            using (var context = new ApplicationDbContext())
            {
                if (!context.Restaurants.Any())
                {
                    var restaurants = new List<Restaurant>
                    {
                        new Restaurant
                        {
                            Name = "Pink Pizza",
                            Location = "Downtown",
                            MenuItems = new List<MenuItem>
                            {
                                new MenuItem { Name = "Margherita Pizza", Description = "Classic tomato and mozzarella", Price = 10.99m },
                                new MenuItem { Name = "Pepperoni Pizza", Description = "Spicy pepperoni with cheese", Price = 12.99m }
                            }
                        },
                        new Restaurant
                        {
                            Name = "Sweet Sushi",
                            Location = "Midtown",
                            MenuItems = new List<MenuItem>
                            {
                                new MenuItem { Name = "California Roll", Description = "Crab, avocado, cucumber", Price = 8.99m },
                                new MenuItem { Name = "Spicy Tuna Roll", Description = "Tuna with spicy mayo", Price = 9.99m }
                            }
                        },
                        new Restaurant
                        {
                            Name = "Cupcake Cafe",
                            Location = "Uptown",
                            MenuItems = new List<MenuItem>
                            {
                                new MenuItem { Name = "Vanilla Cupcake", Description = "Sweet vanilla frosting", Price = 3.99m },
                                new MenuItem { Name = "Chocolate Cupcake", Description = "Rich chocolate delight", Price = 4.29m }
                            }
                        }
                    };
                    context.Restaurants.AddRange(restaurants);
                    context.SaveChanges();
                }
            }
        }
    }
}

using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_MVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new { area = "" });
            }
            else
            {
                if (Session["email"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Home", new { area = "" });

                }
            }
           
        }

        // Logout
        public ActionResult Logout()
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new { area = "" });
            }
            else
            {
                Session.Remove("email");
                return RedirectToAction("Login", "Home", new { area = "" });
            }            
        }

        public ActionResult Error404()
        {
            return View();
        }

    }
}
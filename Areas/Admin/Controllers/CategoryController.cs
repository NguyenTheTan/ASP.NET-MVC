using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_MVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DemoEntities1 db = new DemoEntities1();
        // GET: Admin/Category
        public ActionResult Index()
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new {area = ""});
            }
            else
            {
                ViewBag.Cate = db.Categories.ToList();
                return View();
            }
            
        }

        // Categories
        public ActionResult AddCategory(string Category1)
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {

                return RedirectToAction("Error404", "Home", new {area = ""});
            }
            else
            {
                if (Category1 != null)
                {
                    Category category = new Category() { Category1 = Category1 };
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
                
        }

        //Edit Category
        public ActionResult EditCategory(string Id, string Category1)
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {

                return RedirectToAction("Error404", "Home", new {area = ""});
            }
            else
            {
                Category category = new Category() { Id = Convert.ToInt32(Id), Category1 = Category1 };
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
        }

        // Delete Category
        public ActionResult DeleteCategory(string Id)
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new {area = ""});
            }
            else
            {
                string[] listId = Id.Split(' ');
                foreach (string i in listId)
                {
                    Category category = db.Categories.Find(Convert.ToInt32(i));
                    db.Categories.Remove(category);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
        }
    }
}
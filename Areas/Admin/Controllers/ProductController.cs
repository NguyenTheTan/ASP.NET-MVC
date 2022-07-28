using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_MVC.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly DemoEntities1 db = new DemoEntities1();
        // GET: Admin/Product
        public ActionResult Index()
        {
            if(!(Session["Email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new {area = ""});
            }
            else
            {
                List<Product> listProduct = db.Products.ToList();
                foreach(Product product in listProduct)
                {
                    product.CategoryName = db.Categories.Find(product.CategoryId).Category1;
                }
                ViewBag.Product = listProduct;
                return View();
            }
        }

        public ActionResult AddProduct(Product p)
        {
            if(!(Session["Email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new { area = "" });
            }
            else
            {
                if(ModelState.IsValid && p.ImageName != null)
                {
                    Category category = db.Categories.Where(s => s.Category1 == p.CategoryName).FirstOrDefault<Category>();
                    p.CategoryId = category.Id;
                    string Image = System.IO.Path.GetFileName(p.ImageName.FileName);
                    var path = Server.MapPath("~/Resources/Image/" + Image);
                    p.ImageName.SaveAs(path);
                    p.Images = Image;
                    db.Products.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index", new {area = "Admin"});
                }
                else
                {
                    if(p.ImageName == null)
                    {
                        ModelState.AddModelError("ImageName", "Yêu cầu bắt buộc");
                    }
                }
                return PartialView("_AddProduct");
            }
        }

        public ActionResult EditProduct(Product p)
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new { area = "" });
            }
            else
            {
                if (ModelState.IsValid )
                {
                    Category category = db.Categories.Where(s => s.Category1 == p.CategoryName).FirstOrDefault<Category>();
                    p.CategoryId = category.Id;
                    if(p.ImageName != null)
                    {
                        string Image = System.IO.Path.GetFileName(p.ImageName.FileName);
                        var path = Server.MapPath("~/Resources/Image/" + Image);
                        p.ImageName.SaveAs(path);
                        p.Images = Image;
                    }
                    else
                    {
                        p.Images = db.Products.AsNoTracking().Where(i => i.Id == p.Id).FirstOrDefault().Images;
                    }
                    
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { area = "Admin" });
                }
            }
                return PartialView("_EditProduct");
        }

        public ActionResult DeleteProduct(string Id)
        {
            if (!(Session["email"] is User user) || user.role != 0)
            {
                return RedirectToAction("Error404", "Home", new { area = "" });
            }
            else
            {
                string[] listId = Id.Split(' ');
                foreach(string id in listId)
                {
                    if(id != "")
                    {
                        Product product = db.Products.Find(Convert.ToInt32(id));
                        db.Products.Remove(product);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index", new {area = "Admin"});
            }
        }
    }
}
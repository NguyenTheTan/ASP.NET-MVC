using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DemoEntities1 db = new DemoEntities1();
        public ActionResult Index()
        {
            var product = db.Products.ToList();
            Session["product"] = product;
            ViewBag.pageCount = Math.Ceiling(1.0 * product.Count / 24); 
            return View();
        }

        // Product detail
        public ActionResult ProductDetail(int Id)
        {
            Product productDetail = db.Products.Find(Id);
            return View(productDetail);
        }

        // Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Where(s => s.email == user.email).FirstOrDefault<User>();
                if (u == null)
                {
                    user.role = 1;
                    MD5 pass = new MD5CryptoServiceProvider();

                    pass.ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.password));
                    byte[] result = pass.Hash;

                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        stringBuilder.Append(result[i].ToString("x2"));
                    }
                    user.password = stringBuilder.ToString();
                    user.repeatpassword = stringBuilder.ToString();
                    db.Users.Add(user);
                    db.SaveChanges();

                    Session["email"] = user;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("email", "Tên đăng nhập đã tồn tại");
                }
            }
            return View();
        }

        // Login
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            MD5 pass = new MD5CryptoServiceProvider();
            pass.ComputeHash(ASCIIEncoding.ASCII.GetBytes(u.password));
            byte[] result = pass.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                stringBuilder.Append(result[i].ToString("x2"));
            }
            u.password = stringBuilder.ToString();

            User user = db.Users.Where(s => s.email == u.email && s.password == u.password).FirstOrDefault<User>();
            if(user != null)
            {
                Session["email"] = user;
                if (user.role == 0)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
            return View();
        }


        // Logout
        public ActionResult Logout()
        {
            Session.Remove("email");
            return RedirectToAction("Login","Home");
        }


        // Search
        public ActionResult Search(string searchInput = "")
        {
            var product = db.Products.Where(p => p.Names.Contains(searchInput.ToLower())).Select(p => new {Names = p.Names,Images = p.Images ,Prices = p.Prices, Id = p.Id}).Take(20);
            return Json(product,JsonRequestBehavior.AllowGet);
        }

        //Phân danh mục
        public ActionResult ProductCate(int Id)
        {
            var productCate = db.Products.Where(p => p.CategoryId == Id).ToList();
            ViewBag.Product = productCate;
            ViewBag.pageCount = Math.Ceiling(1.0 * productCate.Count / 24);
            Session["product"] = productCate;
            return View("Index",productCate);
        }

        // Phân trang
        public ActionResult Paginate(int pageNo = 0, int pageSize = 24)
        {
            var list = Session["product"] as List<Product>;
            return PartialView("_Paginate",list.Skip(pageNo * pageSize).Take(pageSize));
        }


        public ActionResult Error404()
        {
            return View();
        }
    }
}
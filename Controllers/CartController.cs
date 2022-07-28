using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly DemoEntities1 db = new DemoEntities1();
        // GET: Cart
        public ActionResult Index()
        {
            if(Session["email"] is User user)
            {
                List<Cart> cart = db.Carts.Where(c => c.UserId == user.id).ToList();
                return View(cart);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult AddToCart(int Id, int Quantity = 1)
        {
            if (Session["email"] is User user)
            {
                Cart cart = db.Carts.Where(c => c.ProductId == Id && c.UserId == user.id).FirstOrDefault();
                    if (cart == null)
                    {
                        cart = new Cart() { ProductId = Id, UserId = user.id, Quantity = Quantity};
                        db.Carts.Add(cart);
                        db.SaveChanges();
                    }
                    else
                    {
                        cart.Quantity += Quantity;
                        db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }

        public ActionResult Increase(int Id)
        {
            Cart cart = db.Carts.Where(c => c.Id == Id).FirstOrDefault();
            cart.Quantity++;
            db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","Cart");
        }

        public ActionResult Decrease(int Id)
        {
            Cart cart = db.Carts.Where(c => c.Id == Id).FirstOrDefault();
            if(cart.Quantity > 1)
            {
                cart.Quantity--;
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Remove", "Cart", new {Id = cart.Id});
            }
        }

        //public ActionResult OrderDetail()
        //{
        //    OrderDetail orderDetail = new OrderDetail();
            
        //    return View();
        //}
        public ActionResult Remove(int Id)
        {
           var cart = db.Carts.Find(Id);  
            
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    
    }
}
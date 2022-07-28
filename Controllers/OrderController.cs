using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly DemoEntities1 db = new DemoEntities1(); 
        // GET: Order
        public ActionResult Index()
        {
            if(Session["email"] is User user )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }

        // Checkout form
        public ActionResult CheckoutForm(Order order)
        {
            User u = Session["email"] as User;
            var user = db.Users.Find(u.id);
            var cart = db.Carts.Where(p => p.UserId == user.id).ToList();
            if (cart.Count() > 0) 
            {
                
                Order od = new Order()
                {
                    UserId = user.id,
                    OrderDate = DateTime.UtcNow,
                    Adds = order.Adds,
                    Amount = user.Carts.Count(),
                    Phones = order.Phones,
                    FullNames = order.FullNames,
                };
                db.Orders.Add(od);
                db.SaveChanges();

                foreach (var cartItem in cart)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.OrderId = od.Id;
                    detail.ProductId = cartItem.ProductId;
                    detail.Quantity = cartItem.Quantity;
                    detail.UnitPrice = cartItem.Product.Prices;
                    db.OrderDetails.Add(detail);
                    db.Carts.Remove(cartItem);
                    db.SaveChanges();
                }
            }
            
            return RedirectToAction("ViewOrder", "Order");
        }

        public ActionResult ViewOrder()
        {
            User user = Session["email"] as User;
            if(user != null)
            {   
                
                var u = db.Users.Find(user.id);
                var orders = db.Orders.Where(o => o.UserId == u.id).ToList();
                ViewBag.User = u;
                ViewBag.View = orders;
                ViewBag.Mess = TempData["Ok"];
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // Order Detail
        public ActionResult OrderDetail(int Id)
        {
            List<OrderDetail> od = db.OrderDetails.Where(o => o.OrderId == Id).ToList();
            ViewBag.Detail = od;
            return View();
        }

        // Hủy Đơn
        public ActionResult CancelOrder(int Id)
        {
            var cancel = db.Orders.Find(Id);
            db.Orders.Remove(cancel);
            var detail = db.OrderDetails.Where(d => d.OrderId == Id).ToList();
            foreach(OrderDetail d in detail)
            {
                db.OrderDetails.Remove(d);
            }
            db.SaveChanges();
            TempData["Ok"] = "Ok";
            return RedirectToAction("ViewOrder","Order");
        }
    }
}
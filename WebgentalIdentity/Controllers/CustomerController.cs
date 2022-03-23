using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebgentalIdentity.Models;
using WebgentalIdentity.Service;

namespace WebgentalIdentity.Controllers
{
    [Authorize(Roles = "CUSTOMER")]
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public CustomerController(IUserService userService, IEmailService emailService, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //cart
        public IActionResult AddtoCart(int id, int quantity, string b1, string b2)
        {
            var userId = _userService.GetUserId();
            if (b1 != null)
            {
                Cart cart = new Cart();
                cart.Uid = userId;
                cart.Pid = id;
                cart.Qauntity_Cart = quantity;
                _db.carts.Add(cart);
                _db.SaveChanges();
                return RedirectToAction("ManageCart");
            }
            else if (b2 != null)
            {

            }
            return View();
        }

        public IActionResult ManageCart()
        {
            var userId = _userService.GetUserId();
            ViewBag.CountCart = _db.carts.Where(x => x.Uid == userId).Count();
            var cartData = _db.carts
                .Include(x => x.Product)
                .Where(x => x.Uid == userId).ToList();

            /*ViewBag.uid = userId;*/
            /*foreach (var item in cartData)
            {*/
            var AllItemTotal = _db.carts.Where(x => x.Uid == userId).Sum(x => x.Product.Pprice * x.Qauntity_Cart);
            ViewBag.sum = AllItemTotal;
            var DeliveryCharge = AllItemTotal * 2 / 100;
            ViewBag.DeliveryCharge = DeliveryCharge;

            if (AllItemTotal >= 80000)
            {
                var Grandtotal = AllItemTotal;
                ViewBag.Grantotal = Grandtotal;
            }
            else
            {
                var Grandtotal = AllItemTotal + DeliveryCharge;
                ViewBag.Grantotal = Grandtotal;
            }
            /*}*/
            /*  var Q = (from P1 in _db.products.ToList() join P2 in _db.carts on P1.Pid equals P2.Pid).ToList();*/
            /* var productdata=_db.products.Where(x=>x.Pid==).*/
            return View(cartData);
        }

        public IActionResult RemoveCartItem(int id)
        {
            var deleteItem = _db.carts.Find(id);
            if (deleteItem != null)
            {
                _db.carts.Remove(deleteItem);
                _db.SaveChanges();
                ViewBag.notice = "Cart Item Not Remove";
                return RedirectToAction("ManageCart");
            }
            else
            {
                ViewBag.notice = "Cart Item Not Remove";
                return RedirectToAction("ManageCart");
            }

        }

        [HttpPost]
        public IActionResult updatecart(int itemQauntity,int Pid,int CartId)
        {
            Cart cartupdate = _db.carts.Find(CartId);
            if (cartupdate != null)
            {
                cartupdate.Qauntity_Cart = itemQauntity;
                _db.carts.Update(cartupdate);
                _db.SaveChanges();
                return RedirectToAction("ManageCart");
            }
            else
            {
                return RedirectToAction("ManageCart");
            }            
        }
        //cart

        [HttpGet]
        public IActionResult CustomerAddress()
        {
            return View();
        }

        //CustomerSettings
        [HttpGet]
        public IActionResult CustomerSettings()
        {
            return View();
        }
        //CustomerSettings
    }
}

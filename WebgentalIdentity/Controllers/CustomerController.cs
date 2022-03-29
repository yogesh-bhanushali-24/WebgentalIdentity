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

        //Address
        [HttpGet]
        public IActionResult CustomerAddress()
        {
            var userId = _userService.GetUserId();
            var AddressAvailable = _db.addressModels.Where(x => x.Uid == userId).FirstOrDefault();
            var Addresscount = _db.addressModels.Where(x => x.Uid == userId).Count();
            if (AddressAvailable != null)
            {
                return RedirectToAction("CustomerConfirmOrder");
            }
            else
            {
                return View();
            }        
        }

        [HttpPost]
        public IActionResult CustomerAddress(AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userService.GetUserId();
                AddressModel address = new AddressModel();
                address.FullName = addressModel.FullName;
                address.State = addressModel.State;
                address.Pincode = addressModel.Pincode;
                address.Address = addressModel.Address;
                address.Landmark = addressModel.Landmark;
                address.Mobile = addressModel.Mobile;
                address.Uid = userId;
                _db.addressModels.Add(address);
                _db.SaveChanges();
                return RedirectToAction("CustomerConfirmOrder");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult MultiAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MultiAddress(AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userService.GetUserId();
                AddressModel address = new AddressModel();
                address.FullName = addressModel.FullName;
                address.State = addressModel.State;
                address.Pincode = addressModel.Pincode;
                address.Address = addressModel.Address;
                address.Landmark = addressModel.Landmark;
                address.Mobile = addressModel.Mobile;
                address.Uid = userId;
                _db.addressModels.Add(address);
                _db.SaveChanges();
                return RedirectToAction("CustomerConfirmOrder");
            }
            else
            {
                return View();
            }
        }

    
        public IActionResult RemoveAddress(int id)
        {
            var RemoveAddress = _db.addressModels.Find(id);
            if (RemoveAddress != null)
            {
                _db.addressModels.Remove(RemoveAddress);
                _db.SaveChanges();
                return RedirectToAction("CustomerConfirmOrder");
            }
            return View();
        }


        [HttpGet]
        public IActionResult EditAddress(int id)
        {
            var Edit = _db.addressModels.Where(x=>x.AddressId==id).FirstOrDefault();
            return View(Edit);

        }

        [HttpPost]
        public IActionResult EditAddress(AddressModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.addressModels.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("CustomerConfirmOrder");
            }
            else
            {
                return View();
            }
        }

        //Address

        //ConfirmOrder

        [HttpGet]
        public IActionResult CustomerConfirmOrder()
        {
            var userId = _userService.GetUserId();
            ViewBag.CountCart = _db.carts.Where(x => x.Uid == userId).Count();
            var cartData = _db.carts
                .Include(x => x.Product)
                .Where(x => x.Uid == userId).ToList();
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

            ViewBag.AllAddress = _db.addressModels.Where(x => x.Uid == userId).ToList();
            return View(cartData);
        }
        //ConfirmOrder

        //CustomerSettings
        [HttpGet]
        public IActionResult CustomerSettings()
        {
          
            return View();
        }
        //CustomerSettings


        //placeorder
        public IActionResult PlaceOrder(int AddressRadios)
        {
            if (AddressRadios > 0) 
            {
                var userId = _userService.GetUserId();
                var cartall = _db.carts.Where(x => x.Uid == userId).ToList();
                foreach (var item in cartall)
                {
                    Orders Placeorder = new Orders();
                    Placeorder.AddressId = AddressRadios;
                    Placeorder.Quantity = item.Qauntity_Cart;
                    Placeorder.Pid = item.Pid;
                    Placeorder.Status = "Pending";
                    Placeorder.Date = DateTime.Now;
                    Placeorder.UserId = userId;
                    _db.Orderss.Add(Placeorder);
                    _db.carts.Remove(item);      
                }
                _db.SaveChanges();
                return RedirectToAction("ShowAllOrders");
            }
            else
            {
                return RedirectToAction("CustomerConfirmOrder");
            }
        }


        public IActionResult ShowAllOrders()
        {
            var userId = _userService.GetUserId();
          
            var AllItemTotal = _db.Orderss.Where(x => x.UserId == userId).Sum(x => x.Product.Pprice * x.Quantity);
            ViewBag.sum = AllItemTotal;
            var ordersDetail = _db.Orderss
            .Include(x => x.Product)
            .Where(x => x.UserId == userId).ToList();
           /* var DeliveryAddress = _db.Orderss.Include(x => x.addressModels).Where(x => x.UserId == userId).ToList();*/

            return View(ordersDetail);
        }
        //placeorder

    }
}

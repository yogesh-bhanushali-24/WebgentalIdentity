using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebgentalIdentity.Models;

namespace WebgentalIdentity.Controllers
{
    [Authorize(Roles = "ADMIN")]
   
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public AdminController(UserManager<ApplicationUser> userManager,ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var count = _userManager.Users.Count();
            ViewBag.count = count;
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("ViewCategory");
            }
            else
            {
                return View();
            }
        }



        [HttpGet]
        public IActionResult ViewCategory(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var sortingcategory = from c in _db.categories select c;

            switch (sortOrder)
            {
                case "name_desc":
                    sortingcategory = sortingcategory.OrderByDescending(c => c.Cname);
                    break;

                default:
                    sortingcategory = sortingcategory.OrderBy(c => c.Cname);
                    break;
            }

            return View(sortingcategory);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var CDelete = _db.categories.Find(id);
            if (CDelete == null)
            {
                return NotFound();
            }
            _db.categories.Remove(CDelete);
            _db.SaveChanges();
            return RedirectToAction("ViewCategory");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var CEdit = _db.categories.Where(x => x.Cid == id).FirstOrDefault();
            return View(CEdit);
        }

        [HttpPost]
        public IActionResult Edit(Category CEdit)
        {
            _db.categories.Update(CEdit);
            _db.SaveChanges();
            return RedirectToAction("ViewCategory");
        }


    }
}

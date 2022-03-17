

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebgentalIdentity.Models;
using WebgentalIdentity.Repository;
using WebgentalIdentity.ViewModel;

namespace WebgentalIdentity.Controllers
{
    [Authorize(Roles = "ADMIN")]
   
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRepository _account;
        

        public AdminController(UserManager<ApplicationUser> userManager,ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager, IAccountRepository account)
        {
            _db = db;
            _userManager = userManager;
            WebHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
            _account = account;
        }

        public IActionResult Index()
        {
            var ShowUsers =  (from user in _db.Users
                                   join userRole in _db.UserRoles
                                   on user.Id equals userRole.UserId
                                   join role in _db.Roles
                                   on userRole.RoleId equals role.Id
                                   where role.Name != "ADMIN"
                                   select user).Count();
           /* var count = _userManager.Users.Count();*/
            ViewBag.count = ShowUsers;

            var Productcount = _db.products.Count();
            ViewBag.Productcount = Productcount;
            return View();
        }

        //category
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

        //category


        //product
        [HttpGet]
        public IActionResult CreateProduct()
        {
            var cat = _db.categories.ToList();
            ViewBag.list = cat;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductViewModel vm)
        {
            var cat = _db.categories.ToList();
            ViewBag.list = cat;
            if (ModelState.IsValid)
            {
                string stringFileName = UploadFile(vm);
                var customer = new Product
                {

                    Pname = vm.Pname,
                    Pdetail = vm.Pdetail,
                    Pprice = vm.Pprice,
                    Cid = vm.Cid,
                    ProfileImage = stringFileName
                };
                _db.products.Add(customer);
                _db.SaveChanges();
                return RedirectToAction("ShowProduct");

            }
            else
            {
                return View();
            }


        }

        private string UploadFile(ProductViewModel vm)
        {
            string fileName = null;
            if (vm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ProfileImage.CopyTo(fileStream);

                }

            }
            return fileName;
        }


        public IActionResult ShowProduct()
        {
            List<Product> custm = _db.products.ToList();
            List<Category> std = _db.categories.ToList();
            var join = from e1 in std
                       join e2 in custm on e1.Cid equals e2.Cid into tabel1
                       from e2 in tabel1.ToList()
                       select new Product_Category
                       {
                           Cate = e1,
                           prod = e2

                       };

            return View(join);
        }
      

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var Edit = _db.products.Where(x => x.Pid == id).FirstOrDefault();
            ViewBag.list = new SelectList(GetCategories(), "Cid", "Cname");
            TempData["Image"] = Edit.ProfileImage;
            ViewBag.Image = TempData.Peek("Image");
            return View(Edit);

        }

        private ICollection GetCategories()
        {
            var res = _db.categories.ToList();
            return res;

        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel prd)
        {
            string file = null;
            if (prd.ProfileImage != null)
            {
                file = UploadFile(prd);
            }
            else if (prd.ProfileImage == null)
            {
                ViewBag.Image = TempData.Peek("Image");
                file = ViewBag.Image;

            }

            if (prd.Pname == null || prd.Pdetail == null || prd.Pprice == null || prd.Cid == null)
            {
                ViewBag.status = false;
                ViewBag.alertmesaage = "Product Updated Failed";

            }
            else
            {
                var product = new Product()
                {
                    Pid = prd.Pid,
                    Pname = prd.Pname,
                    Pdetail = prd.Pdetail,
                    Pprice = prd.Pprice,
                    Cid = prd.Cid,
                    ProfileImage = file
                };
                _db.products.Update(product);
                _db.SaveChanges();
                return RedirectToAction("ShowProduct");
            }
            ViewBag.list = new SelectList(GetCategories(), "cat_id", "category_name");
            ViewBag.Image = TempData.Peek("Image");
            return View();
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id, int cat)
        {
            var delete = _db.products.Where(x => x.Pid == id).FirstOrDefault();
            /*var list = new SelectList(GetCategories(), "Cid", "Cname");*/
            var cat_list = _db.categories.Where(x => x.Cid == cat).FirstOrDefault();
            ViewBag.catlist = cat_list.Cname;
            return View(delete);

        }

        [HttpPost]
        [ActionName("DeleteProduct")]
        public IActionResult DeleteProducts(int id)
        {
            var delete = _db.products.Find(id);
            if (delete == null)
            {
                return NotFound();
            }
            _db.products.Remove(delete);
            _db.SaveChanges();
            return RedirectToAction("ShowProduct");
        }

        public IActionResult DetailsProduct(int id, int cat)
        {
            var Detail = _db.products.Where(x => x.Pid == id).FirstOrDefault();
            /*var list = new SelectList(GetCategories(), "Cid", "Cname");*/
            var cat_list = _db.categories.Where(x => x.Cid == cat).FirstOrDefault();
            ViewBag.catlist = cat_list.Cname;
            return View(Detail);
        }

        //product


        //Users

        [HttpGet]
        public IActionResult AddUsers()
        {
            return View();        
        }
        [HttpPost]
        public async Task<IActionResult> AddUsers(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                await _account.CreateUserAsync(userModel);
                return RedirectToAction("ShowAllUsers");

            }
            return View();
        
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllUsers()
        {
            var Users=_userManager.Users.ToList();
            var ShowUsers = await (from user in _db.Users
                                         join userRole in _db.UserRoles
                                         on user.Id equals userRole.UserId
                                         join role in _db.Roles
                                         on userRole.RoleId equals role.Id
                                         where role.Name != "ADMIN"
                                         select user).ToListAsync();
          return View(ShowUsers);
        }

        [HttpGet]
        public IActionResult DeleteUsers(string id)
        {
            var delete=  _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(delete);
        }

        [HttpPost]
        [ActionName("DeleteUsers")]
        public async Task<IActionResult> DeleteUserss(string id)
        {
            var Delete = await _userManager.FindByIdAsync(id);
            if (Delete != null)
            {
                await _userManager.DeleteAsync(Delete);
                return RedirectToAction("ShowAllUsers");
            }
            return View("ShowAllUsers");
        }

        [HttpGet]
        public IActionResult EditUsers(string id)
        {
          
            var edit = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            
            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsers(ApplicationUser userd)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(userd.Id);
            
            user.FirstName = userd.FirstName;
            user.LastName = userd.LastName;
            user.Email = userd.Email;
            user.UserName = userd.UserName;
            IdentityResult res =await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                return RedirectToAction("ShowAllUsers");
            }
            else
            {
                return View();
            }
            
        }



        //Users


        //Admin
        [HttpGet]
        public IActionResult AdminEdit()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            if (userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> AdminEdit(ApplicationUser userd)
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;
            user.FirstName = userd.FirstName;
            user.LastName = user.LastName;
            user.UserName = user.Email;
            user.Email = user.Email;
            IdentityResult x = await _userManager.UpdateAsync(user);
            return View(userd);
        }

        //Admin











    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebgentalIdentity.Models;
using WebgentalIdentity.Service;

namespace WebgentalIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,IUserService userService,IEmailService emailService, UserManager<ApplicationUser> userManager,ApplicationDbContext db)
        {
            _userService = userService;
            _emailService = emailService;
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index(string sortOrder, int pg = 1, string SearchText = "")
        {
            List<Product> Productss;
            //Search
            if (SearchText != "" && SearchText != null)
            {
                Productss = _db.products.Where(p => p.Pname.Contains(SearchText)).ToList();
            }
            else
            {
                Productss = _db.products.ToList();

                //pagination
                const int pageSize = 9;
                if (pg < 1)
                    pg = 1;

                int recsCount = Productss.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = Productss.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                //paginathion 
                return View(data);
            }
            //Search



            return View(Productss);
        }

        public IActionResult DetailsProduct(int id, int cat)
        {
            var Detail = _db.products.Where(x => x.Pid == id).FirstOrDefault();
            /*var list = new SelectList(GetCategories(), "Cid", "Cname");*/
            var cat_list = _db.categories.Where(x => x.Cid == cat).FirstOrDefault();
            ViewBag.catlist = cat_list.Cname;

            var products = _db.products.ToList();
            ViewBag.products = products;
            return View(Detail);

        }

    
        public IActionResult ShopProduct(string sortOrder, int pg = 1, string SearchText = "")
        {
            List<Product> Productss;
            //Search
            if (SearchText != "" && SearchText != null)
            {
                Productss = _db.products.Where(p => p.Pname.Contains(SearchText)).ToList();
            }
            else
            {
                Productss = _db.products.ToList();

                //pagination
                const int pageSize = 12;
                if (pg < 1)
                    pg = 1;

                int recsCount = Productss.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = Productss.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                //paginathion 
                return View(data);
            }
            //Search



            return View(Productss);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

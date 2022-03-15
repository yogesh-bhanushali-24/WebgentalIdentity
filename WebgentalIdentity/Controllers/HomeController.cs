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

        public HomeController(ILogger<HomeController> logger,IUserService userService,IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _emailService = emailService;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            /* UserEmailOptions options = new UserEmailOptions
             {
                 ToEmails=new List<string>() {"test@gmail.com"},
                 PlaceHolders=new List<KeyValuePair<string, string>>()
                 { 
                     new KeyValuePair<string, string>("{{UserName}}","Yogesh")
                 }

             };
            await _emailService.SendTestEmail(options);*/
          /*  var count = _userManager.Users.Count();
            ViewBag.count = count;*/
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

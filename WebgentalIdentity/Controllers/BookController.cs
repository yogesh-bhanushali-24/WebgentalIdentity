using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebgentalIdentity.Service;

namespace WebgentalIdentity.Controllers
{
    public class BookController : Controller
    {
        private readonly IUserService _userService;

        public BookController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public IActionResult Index()
        {
            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            return View();
        }

    }
}

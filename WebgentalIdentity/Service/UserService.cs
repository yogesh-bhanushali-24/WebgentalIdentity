using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebgentalIdentity.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _ihttpContext;

        public UserService(IHttpContextAccessor ihttpContextAccessor)
        {
            _ihttpContext = ihttpContextAccessor;
        }
        public string GetUserId()
        {
            return _ihttpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return _ihttpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}

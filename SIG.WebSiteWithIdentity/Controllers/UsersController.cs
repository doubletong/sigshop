using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SIG.WebSiteWithIdentity.Services.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SIG.WebSiteWithIdentity.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;

        public UsersController(
            UserManager<AppIdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = this.userManager.Users.ToList();
            return View(viewModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIG.WebAPICore2.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SIG.Data.Entity.Identity;
using SIG.Infrastructure.Helper;
using SIG.Model.Admin.InputModel.Identity;
using SIG.Model.Admin.ViewModel;
using SIG.Repository;
using SIG.Resources.Admin;
using SIG.Services.Identity;

namespace SIG.WebAPICore2.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserServices _userServices;
        public AccountController(IUnitOfWork unitOfWork, IUserServices userServices, ILogger<AccountController> logger)
        {
            _unitOfWork = unitOfWork;
            _userServices = userServices;
            _logger = logger;

           
        }
        //public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser> {
        //    new ApplicationUser{UserName = "darkhelmet", Password = "vespa" },
        //    new ApplicationUser{UserName = "prezscroob", Password = "12345"}
        //};

        public IActionResult Register()
        {
           
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterIM model)
        {
            if (!ModelState.IsValid)
            {
                var errorMes = GetModelErrorMessage();
                AR.Setfailure(errorMes);
                return Json(AR);
               // return Json(false);
            }


            var result = _userServices.CreateUser(model.UserName, model.Email, model.Password, model.DisplayName);

            if (result == 1)
            {
                AR.Setfailure(Messages.CannotRegisterEmail);
                _logger.LogError(Messages.CannotRegisterEmail);
                return Json(AR);
            }

            if (result == 2)
            {
                AR.Setfailure(Messages.CannotRegisterUserName);
                _logger.LogError(Messages.CannotRegisterUserName);
                return Json(AR);
            }


            AR.SetSuccess(string.Format(Messages.AlertCreateSuccess, model.UserName));
            _logger.LogError(string.Format(Messages.AlertCreateSuccess, model.UserName));
            return Json(AR);
        }
        

        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser user, string returnUrl = null)
        {
            //var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "bidianqing") }, CookieAuthenticationDefaults.AuthenticationScheme));
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(180))
            //});
            //return Redirect("/");

            const string badUserNameOrPasswordMessage = "Username or password is incorrect.";
            if (user == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            var lookupUser = _userServices.SignIn(user.UserName, user.Password);
            
                //Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (lookupUser==null)
            {
                ModelState.AddModelError(string.Empty, "无效的用户名或密码！");
               // return BadRequest(badUserNameOrPasswordMessage);
                return View(user);
            }

            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, lookupUser.UserName),
                new Claim(ClaimTypes.Email, lookupUser.Email)
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

           // var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
           // identity.AddClaim(new Claim(ClaimTypes.Name, lookupUser.UserName));
            //add a list of roles
            if (lookupUser.UserRoles!=null)
            {
                var roles = lookupUser.UserRoles.Select(u => u.Role);

                foreach (Role r in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, r.RoleName));
                }
            }

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (returnUrl == null)
            {
                returnUrl = TempData["returnUrl"]?.ToString();
            }
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(/*nameof(HomeController.Index)*/"Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public JsonResult IsUserNameUnique(string UserName)
        {
            var result = _userServices.IsExistUserName(UserName);

            return result
                ? Json(false)
                : Json(true);
        }

        public JsonResult IsEmailUnique(string Email)
        {
            var result = _userServices.IsExistEmail(Email);

            return result
                ? Json(false)
                : Json(true);
        }
        public JsonResult IsEmailUniqueAtEdit(string email, Guid id)
        {
            var result = _userServices.IsExistEmail(email, id);

            return result
                ? Json(false)
                : Json(true);
        }

    }
}
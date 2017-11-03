using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIG.Data.Entity.Identity;
using SIG.Model.Admin.InputModel.Identity;
using SIG.Model.Admin.ViewModel;
using SIG.Repository;
using SIG.Resources.Front;
using SIG.Services.Identity;

namespace SIG.SIGCMS.Controllers
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

        public IActionResult Register()
        {
          
            return View();
        }

        [HttpPost]
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
        public IActionResult Login(string returnUrl)
        {
            var im = new LoginIM
            {
                Username = "admin",
                Password = "123456",
                ReturnUrl = returnUrl

            };
         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginIM im)
        {
            
           
            if (im == null)
            {
                AR.Setfailure(Messages.InvalidUserNameOrPassword);
                return Json(AR);
            }

            var lookupUser = _userServices.SignIn(im.Username, im.Password);

            if (lookupUser == null)
            {
                AR.Setfailure(Messages.InvalidUserNameOrPassword);
                return Json(AR);
            }

            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, lookupUser.UserName),
                new Claim(ClaimTypes.Email, lookupUser.Email)
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

          
            //add a list of roles
            if (lookupUser.UserRoles != null)
            {
                var roles = lookupUser.UserRoles.Select(u => u.Role);

                foreach (Role r in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, r.RoleName));
                }
            }

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = im.RememberMe,
                ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(180))
            });

            AR.SetSuccess(Messages.Wellcome);
            return Json(AR);
        }

        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }

        #region " Ajax "


        public JsonResult IsUserNameUnique(string userName)
        {
            var result = _userServices.IsExistUserName(userName);

            return result
                ? Json(false)
                : Json(true);
        }

        public JsonResult IsEmailUnique(string email)
        {
            var result = _userServices.IsExistEmail(email);

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

        #endregion


    }


}
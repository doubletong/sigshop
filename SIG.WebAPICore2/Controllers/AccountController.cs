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

namespace SIG.WebAPICore2.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUnitOfWork unitOfWork, ILogger<AccountController> logger)
        {
            _unitOfWork = unitOfWork;
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


            var result = CreateUser(model.UserName, model.Email, model.Password, model.DisplayName);

            if (result == 1)
            {
                AR.Setfailure(Messages.CannotRegisterEmail);
                return Json(AR);
            }

            if (result == 2)
            {
                AR.Setfailure(Messages.CannotRegisterUserName);
                return Json(AR);
            }


            AR.SetSuccess(string.Format(Messages.AlertCreateSuccess, model.UserName));
            return Json(AR);
        }


        protected int CreateUser(string userName, string email, string password, string realName)
        {
            var orgUsers = _unitOfWork.GetRepository<User>().GetFirstOrDefault(u=>u,u => u.Email == email);
            if (orgUsers!=null)
            {
                return 1; //1 邮箱已存在
            }

            orgUsers = _unitOfWork.GetRepository<User>().GetFirstOrDefault(u => u, u => u.UserName == userName);
            if (orgUsers !=null)
            {
                return 2; //1 用户名已存在
            }


            var securityStamp = Hash.GenerateSalt();
            var passwordHash = Hash.HashPasswordWithSalt(password, securityStamp);

            var newUser = new User()
            {
                UserName = userName,
                RealName = realName,
                Email = email,
                SecurityStamp = Convert.ToBase64String(securityStamp),
                PasswordHash = passwordHash,
                CreateDate = DateTime.Now,
                IsActive = true
            };

            _logger.LogInformation(string.Format(Logs.CreateMessage, EntityNames.User, userName));
            _unitOfWork.GetRepository<User>().Insert(newUser);
            _unitOfWork.SaveChanges();
            // SetUserCookies(false, newUser);

            return 0;

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
            var lookupUser = _unitOfWork.GetRepository<User>().GetFirstOrDefault<User>(d=>d, d => d.UserName == user.UserName);
                //Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (lookupUser?.PasswordHash != user.Password)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }
          
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, lookupUser.UserName));
            //add a list of roles
            //foreach (Role r in someList.Roles)
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, r.Name));
            //}

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.Add((TimeSpan.FromDays(180)))
            });

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

      
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fiver.Security.Authentication.Models.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SIG.WebAPICore2.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Login(string requestPath)
        {
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            if (!IsAuthentic(inputModel.Username, inputModel.Password))
                return View();

            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Sean Connery"),
                new Claim(ClaimTypes.Email, inputModel.Username)
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "FiverSecurityScheme");

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                scheme: "FiverSecurityScheme",
                principal: principal,
                properties: new AuthenticationProperties
                {
                    IsPersistent = true, // for 'remember me' feature
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                });

           // return Redirect(inputModel.RequestPath ?? "/");
            //return RedirectToAction("Index", "Home");
            var userName = HttpContext.User.Identity.Name;
            TempData["UserName"] = userName;
            return View();
        }

        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                scheme: "FiverSecurityScheme");

            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }

        #region " Private "

        private bool IsAuthentic(string username, string password)
        {
            return (username == "james" && password == "bond");
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SIG.SIGCMS.Controllers
{
    public class ErrorController : BaseController
    {
        public IActionResult Index(int statusCode)
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            ViewBag.StatusCode = statusCode;
            ViewBag.OriginalPath = feature?.OriginalPath;
            ViewBag.OriginalQueryString = feature?.OriginalQueryString;

            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace SIG.SIGCMS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            GlobalDiagnosticsContext.Set("CreatedBy", "doubletong");
            _logger.LogInformation("nlog is working from a controller");

            return View();
        }
    }
}
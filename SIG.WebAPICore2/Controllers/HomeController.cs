using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NLog;


namespace SIG.WebAPICore2.Controllers
{
    //[ServiceFilter(typeof(LogFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }
        public IActionResult Index()
        {
            GlobalDiagnosticsContext.Set("CreatedBy", "doubletong");
            _logger.LogCritical("nlog is working from a controller");         
          
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            
            return View();
        }
    }
}
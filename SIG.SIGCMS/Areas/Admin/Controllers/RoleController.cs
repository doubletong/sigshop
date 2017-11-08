using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIG.SIGCMS.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
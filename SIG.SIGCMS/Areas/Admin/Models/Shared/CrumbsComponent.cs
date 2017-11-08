using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIG.Infrastructure.Configs;
using SIG.Services.Menus;

namespace SIG.SIGCMS.Areas.Admin.Models.Shared
{
    [ViewComponent(Name = "Crumbs")]
    public class CrumbsComponent : ViewComponent
    {
        private readonly IMenuServices _menuServices;

        public CrumbsComponent(IMenuServices menuServices)
        {
            _menuServices = menuServices;

        }
        public IViewComponentResult Invoke(ViewContext viewContext)
        {
            return View("Crumbs", _menuServices.CurrenMenuCrumbs(SettingsManager.Menu.BackMenuCId, viewContext));
        }
    }
}

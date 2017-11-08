using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIG.Infrastructure.Configs;
using SIG.Model.Admin.ViewModel.Menus;
using SIG.Services.Menus;

namespace SIG.SIGCMS.Areas.Admin.Models.Shared
{
    [ViewComponent(Name = "LeftNav")]
    public class LeftNavComponent : ViewComponent
    {
        private readonly IMenuServices _menuServices;

        public LeftNavComponent(IMenuServices menuServices)
        {
            _menuServices = menuServices;

        }
        public IViewComponentResult Invoke(int categoryId, ViewContext viewContext)
        {
            LeftNavVM vm = new LeftNavVM
            {
                Menus = _menuServices.GetShowMenus(categoryId),
                CurrentMenu = _menuServices.GetCurrenMenu(viewContext)
            };
            return View(vm);
        }
    }
}

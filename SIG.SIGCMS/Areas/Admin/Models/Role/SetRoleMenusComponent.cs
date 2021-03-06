﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SIG.Data.Enums;
using SIG.Infrastructure.Configs;
using SIG.Services.Identity;
using SIG.Services.Menus;

namespace SIG.SIGCMS.Areas.Admin.Models.Menu
{
    [ViewComponent(Name = "RoleMenus")]
    public class SetRoleMenusComponent : ViewComponent
    {
        private readonly IMenuServices _menuServices;
        private readonly IRoleServices _roleServices;

        public SetRoleMenusComponent(IMenuServices menuServices, IRoleServices roleServices)
        {
            _menuServices = menuServices;
            _roleServices = roleServices;

        }
        public IViewComponentResult Invoke(int id)
        {
            var role = _roleServices.GetByIdWithRoleMenu(id);
            var menus = _menuServices.GetMenusByCategoryId(SettingsManager.Menu.BackMenuCId);
            int[] menuIds = role.RoleMenus.Select(m => m.MenuId).ToArray();

            //SetRoleMenusVM vm = new SetRoleMenusVM
            //{
            //    RoleId = id,
            //    Menus = menus,
            //    MenuIds = menuIds
            //};
           
            var MenuTree = CreatedMenuList(menus.Where(m=>m.ParentId == null), menuIds);

            return View("Default",MenuTree);
           // return View("Menus", _menuServices.CurrenMenuCrumbs(SettingsManager.Menu.BackMenuCId, viewContext));
        }

        private string CreatedMenuList(IEnumerable<Data.Entity.Identity.Menu> menus, int[] menuIds, string menuTree = "")
        {
            
            menuTree = menuTree + $"<ul class=\"menuTree menuTreeRole list-unstyled\">";
            foreach(var item in menus.OrderBy(m=>m.Importance))
            {
                if ((item.MenuType == MenuType.PAGE && item.LayoutLevel < 2) || item.MenuType == MenuType.NOLINK)
                {
                    if (item.Active)
                    {
                        var className = item.ChildMenus.Any() ? "hasMenus" : "";
                        menuTree = menuTree + $"<li class=\"{className}\">";
                        menuTree = menuTree + $"<label>";
                        var isChecked = menuIds!=null && menuIds.Contains(item.Id) ? "checked" : "";
                        menuTree = menuTree + $"<input type = \"checkbox\" name=\"menuId\" value=\"{item.Id}\"  {isChecked} />";
                        menuTree = menuTree + item.Title;
                        menuTree = menuTree + $"</label>";
                         
                        if(item.ChildMenus.Any())
                        {
                            menuTree =  CreatedMenuList(item.ChildMenus, menuIds, menuTree);
                        }
                        menuTree = menuTree + $"</li>";

                    }

                }
                else
                {
                    var className = item.ChildMenus.Any() ? "hasMenus" : "";
                    menuTree = menuTree + $"<li class=\"{className}\">";
                    menuTree = menuTree + $"</label>";

                    var isChecked = menuIds.Contains(item.Id) ? "checked" : "";
                    menuTree = menuTree + $"<input type = \"checkbox\" name=\"menuId\" value=\"{item.Id}\" {isChecked} />";
                    menuTree = menuTree + item.Title;
                    menuTree = menuTree + $"</label>";

                    if (item.ChildMenus.Any())
                    {
                        menuTree = CreatedMenuList(item.ChildMenus, menuIds, menuTree);
                    }
                    menuTree = menuTree + $"</li>";
                }


            }

            menuTree = menuTree + $"</ul>";
            return menuTree;
        }
    }
}

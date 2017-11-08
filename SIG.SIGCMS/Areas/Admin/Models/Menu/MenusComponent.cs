using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIG.Infrastructure.Configs;
using SIG.Services.Menus;

namespace SIG.SIGCMS.Areas.Admin.Models.Menu
{
    [ViewComponent(Name = "MenuList")]
    public class MenusComponent : ViewComponent
    {
        private readonly IMenuServices _menuServices;

        public MenusComponent(IMenuServices menuServices)
        {
            _menuServices = menuServices;

        }
        public IViewComponentResult Invoke(int categoryId)
        {
            var menus = _menuServices.GetLevelMenusByCategoryId(categoryId);
            var MenuTree = CreatedMenuList(menus.Where(m => m.ParentId ==null),menus);

            return View("MenuList", MenuTree);
           // return View("Menus", _menuServices.CurrenMenuCrumbs(SettingsManager.Menu.BackMenuCId, viewContext));
        }

        private string CreatedMenuList(IEnumerable<Data.Entity.Identity.Menu> levelMenus, IEnumerable<Data.Entity.Identity.Menu> menus,string menuTree = "", bool isExpand = true)
        {
            var exClassName = isExpand ? "" : "hidden";
            menuTree = menuTree + $"<ul class=\"menuTree {exClassName}\">";
            foreach(var item in levelMenus)
            {
                item.ChildMenus = menus.Where(m => m.ParentId == item.Id).OrderBy(c => c.Importance).ToList();
                var hasChild = item.ChildMenus.Any();
                var hasMenusClassName = hasChild ? "hasMenus" : "";
                menuTree = menuTree + $"<li class=\"item-container {hasMenusClassName}\"><div>";
                if (hasChild)
                {
                    var url = Url.Action("IsExpand", new { id = item.Id });
                    if (isExpand)
                    {
                        
                        menuTree = menuTree + $"<a href = \"#\" class=\"expmenu expandmenu\" data-url=\"{url}\"><span class=\"fa fa-minus-circle\"></span></a>";
                    }
                    else
                    {
                        menuTree = menuTree + $"<a href = \"#\" class=\"expmenu\" data-url=\"{url}\"><span class=\"fa fa-plus-circle\"></span></a>";
                    }
                }
                menuTree = menuTree + item.Title;
                menuTree = menuTree + $"<a href = \"{Url.Action("UpDownMove", new { id = item.Id, isUp = true, categoryId = item.CategoryId })}\" class=\"moveMenu\" title=\"向上\" data-id=\"{item.Id}\" data-categoryid=\"{item.CategoryId}\"><i class=\"fa fa-chevron-up\"></i></a>";
                menuTree = menuTree + $"<a href = \"{Url.Action("UpDownMove", new { id = item.Id, isUp = false, categoryId = item.CategoryId })}\" class=\"moveMenu\" title=\"向下\" data-id=\"{item.Id}\" data-categoryid=\"{item.CategoryId}\"><i class=\"fa fa-chevron-down\"></i></a>";
                menuTree = menuTree +
                           $"<a href=\"{Url.Action("MoveMenu", "Menu")}\" data-route-id= \"{item.Id}\" data-ajax = \"true\" title = \"移动菜单\" " +
                           $"data-ajax-method = \"GET\" data-ajax-mode = \"replace\" data-ajax-update=\"#edit-container\" data-ajax-begin = \"onBegin\" " +
                           $"data-ajax-complete = \"onComplete\" data-ajax-failure = \"onFailed\" data-ajax-success = \"onSuccess\">" +
                           $"<i class=\"fa fa-chevron-right\"></i></a>";

                menuTree = menuTree +
                           $"<a href=\"{Url.Action("CreateMenu", "Menu",new{parentId = item.Id,categoryId = item.CategoryId})}\"  " +
                           $" data-ajax = \"true\" title = \"新增子菜单\" " +
                           $"data-ajax-method = \"GET\" data-ajax-mode = \"replace\" data-ajax-update=\"#edit-container\" data-ajax-begin = \"onBegin\" " +
                           $"data-ajax-complete = \"onComplete\" data-ajax-failure = \"onFailed\" data-ajax-success = \"onSuccess\">" +
                           $"<i class=\"fa fa-plus\"></i></a>";

                menuTree = menuTree +
                           $"<a href=\"{Url.Action("EditMenu", "Menu",new{id=item.Id})}\"  data-ajax = \"true\" title = \"编辑菜单\" " +
                           $"data-ajax-method = \"GET\" data-ajax-mode = \"replace\" data-ajax-update=\"#edit-container\" data-ajax-begin = \"onBegin\" " +
                           $"data-ajax-complete = \"onComplete\" data-ajax-failure = \"onFailed\" data-ajax-success = \"onSuccess\">" +
                           $"<i class=\"fa fa-pencil\"></i></a>";

                if(item.Active)
                {
                    menuTree = menuTree +
                               $"<a href = \"#\" data-url = \"{Url.Action("IsActive", new {id = item.Id})}\" class=\"active-item\" title=\"锁定\" data-action=\"激活\"> " +
                               $"<i class=\"fa fa-eye-slash\"></i></a>";
                }
                else
                {
                    menuTree = menuTree +
                               $"<a href = \"#\" data-url = \"{Url.Action("IsActive", new { id = item.Id })}\" class=\"active-item\" title=\"激活\" data-action=\"锁定\"> " +
                               $"<i class=\"fa fa-eye\"></i></a>";
                }
                menuTree = menuTree +
                           $"<a href = \"#\" data-url=\"{Url.Action("Delete", new { id = item.Id})}\" class=\"delete-item\" title=\"移除菜单\"" +
                           $" data-id=\"{item.Id}\" data-categoryid=\"{item.CategoryId}\"><i class=\"fa fa-trash\"></i></a>";
                


                menuTree = menuTree + $"</div>";
                if(hasChild)
                {
                    menuTree = CreatedMenuList(item.ChildMenus,menus, menuTree, item.IsExpand);
                }
                menuTree = menuTree + $"</li>";
            }

            menuTree = menuTree + $"</ul>";
            return menuTree;
        }
    }
}

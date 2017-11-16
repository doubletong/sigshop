using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIG.Data.Enums;
using SIG.Infrastructure.Configs;
using SIG.Repository;
using System.Linq;
using System.Threading.Tasks;
using SIG.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using SIG.Data.Entity.Identity;

namespace SIG.Services.Menus
{
    public class MenuServices : IMenuServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Menu> CurrenMenuCrumbs(int categoryId, ViewContext viewContext)
        {
            var controller = viewContext.RouteData.Values["controller"].ToString();
            var action = viewContext.RouteData.Values["action"].ToString();

            var area = string.Empty;
            object areaObj;
            if (viewContext.RouteData.Values.TryGetValue("area", out areaObj))
            {
                area = areaObj.ToString();
            }
            // string area = Site.CurrentArea(), controller = Site.CurrentController(), action = Site.CurrentAction();
            //string actionName = ControllerContext.RouteData.Values["action"].ToString();
            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            //var aa= ControllerContext.ActionDescriptor.ActionName

            var rource = GetShowMenus(categoryId);
            List<Menu> menus = new List<Menu>();

            Menu vMenu = rource.FirstOrDefault(m => area.Equals(m.Area, StringComparison.OrdinalIgnoreCase)
            && controller.Equals(m.Controller, StringComparison.OrdinalIgnoreCase)
            && action.Equals(m.Action, StringComparison.OrdinalIgnoreCase));

            if (vMenu != null)
                RecursiveLoad(vMenu, menus);

            return menus;
        }

        /// <summary>
        /// 递归获取父项
        /// </summary>
        /// <param name="vMenu"></param>
        /// <param name="Parents"></param>
        private void RecursiveLoad(Menu vMenu, List<Menu> Parents)
        {
            Parents.Insert(0, vMenu);
            if (vMenu.ParentId != null)
            {
                var rource = GetShowMenus(vMenu.CategoryId);
                Menu parentMenu = rource.FirstOrDefault(m => m.Id == vMenu.ParentId);
                if (parentMenu != null)
                    RecursiveLoad(vMenu.ParentMenu, Parents);
            }
        }


        public List<Menu> GetShowMenus(int categoryId)
        {

            var menus = _unitOfWork.GetRepository<Menu>().
                GetMany(predicate: m => m.CategoryId == categoryId && (m.MenuType == MenuType.NOLINK ||
                m.MenuType == MenuType.PAGE), orderBy: d => d.OrderBy(m => m.Importance), include: d=>d.Include(m=>m.ParentMenu)).ToList();

            return menus;

        }
        public IEnumerable<Menu> GetLeftMenus(int categoryId)
        {

            var menus = _unitOfWork.GetRepository<Menu>().
                GetMany(predicate: m => m.CategoryId == categoryId && m.Active && 
                (m.MenuType == MenuType.NOLINK || m.MenuType == MenuType.PAGE) && m.LayoutLevel < 2, 
                                                                       orderBy: d => d.OrderBy(m => m.Importance),
                                                                       include: d => d.Include(m => m.ChildMenus));

            return menus;

        }

        /// <summary>
        /// 获取需要高亮的菜单ID
        /// </summary>
        /// <returns></returns>
        public Menu GetCurrenMenu(ViewContext viewContext)
        {
            var controller = viewContext.RouteData.Values["controller"].ToString();
            var action = viewContext.RouteData.Values["action"].ToString();

            var area = string.Empty;
            object areaObj;
            if (viewContext.RouteData.Values.TryGetValue("area", out areaObj))
            {
                area = areaObj.ToString();
            }
            //string area = Site.CurrentArea(), controller = Site.CurrentController(), action = Site.CurrentAction();

            var menus = GetAllMenusByCategoryId(SettingsManager.Menu.BackMenuCId);

            Menu vMenu = menus?.FirstOrDefault(m => area.Equals(m.Area, StringComparison.OrdinalIgnoreCase)
                                                                         && controller.Equals(m.Controller, StringComparison.OrdinalIgnoreCase)
                                                                         && action.Equals(m.Action, StringComparison.OrdinalIgnoreCase));


            if (vMenu == null)
                return null;

            if (vMenu.Active || vMenu.MenuType == MenuType.PAGE)
                return vMenu;

            return RecursiveLoadMenu(vMenu.ParentId);


        }


        private Menu RecursiveLoadMenu(int? parentId)
        {
            var menus = GetAllMenusByCategoryId(SettingsManager.Menu.BackMenuCId);

            Menu vMenu = menus.FirstOrDefault(m => m.ParentId == parentId && m.MenuType == MenuType.PAGE);

            if (vMenu.ParentMenu != null && (vMenu.ParentMenu.MenuType != MenuType.PAGE || !vMenu.ParentMenu.Active))
            {
                return RecursiveLoadMenu(vMenu.ParentId);
            }
            return vMenu.ParentMenu;
        }


        public List<Menu> GetAllMenusByCategoryId(int categoryId)
        {
            var menus = _unitOfWork.GetRepository<Menu>().GetMany(m => m.CategoryId == categoryId).ToList();
            return menus;
        }

        /// <summary>
        /// 按分类ID获取菜单
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetMenusByCategoryId(int categoryId)
        {
            return _unitOfWork.GetRepository<Menu>().GetMany(predicate: m => m.CategoryId == categoryId,
                orderBy:d=>d.OrderBy(m=>m.Importance), 
                include:d=>d.Include(m=>m.ChildMenus));
        }

        public async Task<IEnumerable<Menu>> GetRolesMenusByUserId(Guid userId)
        {
            var roles = await _unitOfWork.GetRepository<UserRole>().GetManyAsync(predicate: d => d.UserId == userId);
            var roleIds = roles.Select(d => d.RoleId);
            var menus = await _unitOfWork.GetRepository<RoleMenu>().GetManyAsync(predicate: m => roleIds.Contains(m.RoleId));
            var menuIds = menus.Select(d => d.MenuId);
            return await _unitOfWork.GetRepository<Menu>().GetManyAsync(predicate:d=>menuIds.Contains(d.Id));
        }

        /// <summary>
        /// 按分类ID获取菜单并缓存
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        //public IEnumerable<Menu> GetMenusByCategoryIdWithCache(int categoryId)
        //{
        //    var key = $"{EntityNames.Menu}S_SetRole_{categoryId}";

        //    IEnumerable<Menu> result;
        //    if (SettingsManager.Menu.EnableCaching)
        //    {
        //        if (_cacheService.IsSet(key))
        //        {
        //            result = (IEnumerable<Menu>)_cacheService.Get(key);
        //        }
        //        else
        //        {
        //            result = GetMenusByCategoryId(categoryId);
        //            _cacheService.Set(key, result, SettingsManager.Menu.CacheDuration);
        //        }
        //    }
        //    else
        //    {
        //        result = GetMenusByCategoryId(categoryId);
        //    }

        //    return result;
        //}

        /// <summary>
        /// 按分类ID获取菜单并层级化（角色菜单权限）
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetLevelMenusByCategoryId(int categoryId)
        {
            var menus = _unitOfWork.GetRepository<Menu>().GetMany(
                predicate: d => d.CategoryId == categoryId,disableTracking:false);
            
            return menus;
        }


        /// <summary>
        /// 按分类重设排序
        /// </summary>
        /// <param name="categoryId"></param>
        public void ResetSort(int categoryId)
        {
            var menuList = _unitOfWork.GetRepository<Menu>().GetMany(predicate: d => d.CategoryId == categoryId, include: d => d.Include(m => m.ChildMenus));
            var list = menuList.Where(m => m.ParentId == null).OrderBy(m => m.Importance)
                .SelectDeep<Menu>(m => m.ChildMenus.OrderBy(g => g.Importance));

            int i = 0;
            foreach (var item in list)
            {
                var menu = GetById(item.Id);
                menu.Importance = i;
                _unitOfWork.GetRepository<Menu>().Update(menu);
                i++;
            }
            _unitOfWork.SaveChanges();
        
        }

        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name = "id" ></param >
        /// < returns ></returns >
        public int UpMoveMenu(int id)
        {
            var vMenu = _unitOfWork.GetRepository<Menu>().GetFirstOrDefault(predicate: d => d.Id == id);
            var menuList = GetMenusByCategoryId(vMenu.CategoryId).OrderBy(m => m.Importance);

            Menu prevMenu = _unitOfWork.GetRepository<Menu>().GetFirstOrDefault(
                predicate: d => d.ParentId == vMenu.ParentId && d.Id !=vMenu.Id && d.Importance <= vMenu.Importance,
                orderBy: d => d.OrderByDescending(m => m.Importance));


            //if (prevMenu == null)
            //{
            //    // 已经在第一位
            //    return 0;
            //}
            var num = prevMenu.Importance - vMenu.Importance;
            if (num == 0)
            {
                vMenu.Importance = vMenu.Importance - 1;
            }
            else
            {
                prevMenu.Importance = prevMenu.Importance - num;
                vMenu.Importance = vMenu.Importance + num;
            }
           

            Update(prevMenu);
            Update(vMenu);
            // _unitOfWork.SaveChanges();

            // ResetSort(vMenu.CategoryId);

            return 1;
        }

        /// <summary>
        /// 向下移动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DownMoveMenu(int id)
        {
            var vMenu = _unitOfWork.GetRepository<Menu>().GetFirstOrDefault(predicate: d=>d.Id == id);
            var menuList = GetMenusByCategoryId(vMenu.CategoryId).OrderBy(m => m.Importance); 

            Menu nextMenu = _unitOfWork.GetRepository<Menu>().GetFirstOrDefault(
                predicate: d => d.ParentId == vMenu.ParentId && d.Id != vMenu.Id && d.Importance >= vMenu.Importance,
                orderBy: d => d.OrderBy(m => m.Importance));
            //Menu nextMenu = menuList.Where(m => m.ParentId == vMenu.ParentId &&
            //    m.Importance > vMenu.Importance).OrderBy(m => m.Importance).FirstOrDefault();


            //if (nextMenu == null)
            //{
            //    // 已经在最后一位
            //    return 0;
            //}
            
            var num = nextMenu.Importance - vMenu.Importance;
            if (num == 0)
            {
                vMenu.Importance = vMenu.Importance + 1;
            }
            else
            {
                nextMenu.Importance = nextMenu.Importance - num;
                vMenu.Importance = vMenu.Importance + num;
            }
           

            Update(vMenu);
            Update(nextMenu);
           // _unitOfWork.SaveChanges();

           // ResetSort(vMenu.CategoryId);
            return 1;
        }



        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        //        public int SetMenuImportance(int menuId, int num)
        //        {
        //            int result;
        //            using (_connection = Utilities.GetOpenConnection())
        //            {
        //                var query = "with MenuSet_tree as ( " +
        //  "select MenuSet.id as top_MenuSet_id, MenuSet.id as MenuSet_id " +
        //  " from MenuSet union all   select top_MenuSet_id, MenuSet.id   from MenuSet_tree " +
        //  "     join MenuSet on MenuSet.ParentId = MenuSet_tree.MenuSet_id)  " +
        //"update MenuSet Set Importance = Importance + @Num where Id in (select MenuSet_id from MenuSet_tree where top_MenuSet_id = @MenuId)";
        //                result = (int)_connection.Execute(query, new { MenuId = menuId, Num = num });
        //            }
        //            return result;
        //        }


        public Menu GetById(int id)
        {
            return _unitOfWork.GetRepository<Menu>().Find(id);
        }
        public Menu GetByIdWithChilds(int id)
        {
            return _unitOfWork.GetRepository<Menu>().GetFirstOrDefault(predicate: d => d.Id == id,include: d=>d.Include(m=>m.ChildMenus));
        }


        public bool Update(Menu menu)
        {
             _unitOfWork.GetRepository<Menu>().Update(menu);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Create(Menu menu)
        {
            _unitOfWork.GetRepository<Menu>().Insert(menu);
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool Delete(Menu menu)
        {
            _unitOfWork.GetRepository<Menu>().Delete(menu);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}

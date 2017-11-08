
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIG.Services.Menus
{
    public interface IMenuServices
    {
        //List<Menu> GetFrontMenus(int categoryId);
        //Menu CreateAndSort(Menu menu);
        //// Menu UpdateAndSort(Menu menu);
        //void ResetSort(int categoryId);
        //Menu GetMenuWithChildMenus(int Id);
        List<Data.Entity.Identity.Menu> GetShowMenus(int categoryId);

        //Task<IEnumerable<Menu>> GetMenus(int categoryId, CancellationToken cancellationToken = default(CancellationToken));
        //int UpMoveMenu(int id);
        //int DownMoveMenu(int id);
        //List<Menu> GetFaltMenus(int categoryId);
        ////List<MenuVM> GetFaltMenus(int categoryId);

        IEnumerable<Data.Entity.Identity.Menu> GetMenusByCategoryId(int categoryId);
        IEnumerable<Data.Entity.Identity.Menu> GetLevelMenusByCategoryId(int categoryId);
        Data.Entity.Identity.Menu GetByIdWithChilds(int id);
        int UpMoveMenu(int id);
        int DownMoveMenu(int id);

        List<Data.Entity.Identity.Menu> CurrenMenuCrumbs(int categoryId, ViewContext viewContext);

        Data.Entity.Identity.Menu GetCurrenMenu(ViewContext viewContext);
        void ResetSort(int categoryId);

        Data.Entity.Identity.Menu GetById(int id);
        bool Update(Data.Entity.Identity.Menu menu);
        bool Create(Data.Entity.Identity.Menu menu);
        bool Delete(Data.Entity.Identity.Menu menu);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SIG.Data.Entity.Identity;

namespace SIG.Services.Menus
{
    public interface IMenuCategoryServices
    {
        MenuCategory GetById(int id);
    }
}

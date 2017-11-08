using System;
using System.Collections.Generic;
using System.Text;
using SIG.Data.Entity.Identity;
using SIG.Repository;

namespace SIG.Services.Menus
{
    public class MenuCategoryServices: IMenuCategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public MenuCategory GetById(int id)
        {
            return _unitOfWork.GetRepository<MenuCategory>().Find(id);
        }
    }
}

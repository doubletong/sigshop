using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SIG.Data.Entity.Identity;
using SIG.Repository;
using SIG.Services.Identity;

namespace SIG.Services.Identity
{
    public class RoleServices:IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Role GetById(int id)
        {
            return _unitOfWork.GetRepository<Role>().Find(id);
        }
        public Role GetByIdWithRoleMenu(int id,bool disableTracking = true)
        {
            return _unitOfWork.GetRepository<Role>().GetFirstOrDefault(predicate:d=>d.Id == id,include: d=>d.Include(r=>r.RoleMenus), disableTracking: disableTracking);
        }


        public IEnumerable<Role> GetAll()
        {
            var menus = _unitOfWork.GetRepository<Role>().GetMany(disableTracking:false);
            return menus;
        }

        public IEnumerable<Role> GetRolesByUserId(Guid userId)
        {
            var menus = _unitOfWork.GetRepository<Role>().GetMany(predicate: d=>d.UserRoles.Any(u=>u.UserId == userId),include:d=>d.Include(m=>m.UserRoles));
            return menus;
        }

        public void SetRoleMenus(int RoleId, int[] menuId)
        {
           
            var rolemenus = _unitOfWork.GetRepository<RoleMenu>().GetMany(d => d.RoleId == RoleId);
            //vRole.RoleMenus.Clear();
            _unitOfWork.GetRepository<RoleMenu>().Delete(rolemenus);
            _unitOfWork.SaveChanges();
            if (menuId != null)
            {
                foreach (var mid in menuId)
                {
                    _unitOfWork.GetRepository<RoleMenu>().Insert(new RoleMenu { RoleId = RoleId, MenuId = mid });
                }
            }
            _unitOfWork.SaveChanges();

            //var key = $"{EntityNames.Menu}s";
            //_cacheService.Invalidate(key); //取消缓存

        }

        public void Update(Role role)
        {
             _unitOfWork.GetRepository<Role>().Update(role);
            _unitOfWork.SaveChanges();
        }

        public void Create(Role role)
        {
             _unitOfWork.GetRepository<Role>().Insert(role);
            _unitOfWork.SaveChanges();
        }
        public void Delete(Role role)
        {
            _unitOfWork.GetRepository<Role>().Delete(role);
            _unitOfWork.SaveChanges();

        }
    }
}

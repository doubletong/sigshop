using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<Role> GetAll()
        {
            var menus = _unitOfWork.GetRepository<Role>().GetMany(disableTracking:false);
            return menus;
        }
    }
}

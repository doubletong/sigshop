using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIG.Data.Entity.Identity
{
    
    public partial class Role
    {

        public int Id { get; set; }
       
        public string RoleName { get; set; }
      
        public string Description { get; set; }
        public bool IsSys { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }


    }
}

using System;
using System.Collections.Generic;
using SIG.Data.Entity.Identity;


namespace TZGCMS.Model.Admin.ViewModel.Identity
{

    public class SetUserRolesVM
    {
        public Guid UserId { get; set; }
        public int[] RoleIds { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}

using System.Collections.Generic;
using SIG.Data.Entity.Identity;

namespace TZGCMS.Model.Admin.ViewModel.Identity
{
    public class SetRoleMenusVM
    {
        public int[] MenuIds { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
        public int RoleId { get; set; }
    }
}

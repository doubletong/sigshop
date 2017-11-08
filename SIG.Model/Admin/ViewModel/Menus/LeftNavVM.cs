using System.Collections.Generic;
using SIG.Data.Entity.Identity;


namespace SIG.Model.Admin.ViewModel.Menus
{
    public class LeftNavVM
    {     
        public IEnumerable<Menu> Menus { get; set; }
        public Menu CurrentMenu { get; set; }
    }
}

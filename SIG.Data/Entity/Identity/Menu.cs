using SIG.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIG.Data.Entity.Identity
{
    public partial class Menu : IAuditedEntity
    {
        public int Id { get; set; }
        public Menu()
        {

            this.ChildMenus = new HashSet<Menu>();
            this.MenuType = MenuType.PAGE;
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public MenuType MenuType { get; set; }
        public string Iconfont { get; set; }
        public int Importance { get; set; }

        public bool Active { get; set; }


        public bool IsExpand { get; set; }
        public int? ParentId { get; set; }
        public virtual Menu ParentMenu { get; set; }

        public virtual ICollection<Menu> ChildMenus { get; set; }

        public int CategoryId { get; set; }
        public virtual MenuCategory Category { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public int? LayoutLevel { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        //public IEnumerable<Menu> ParentMenus()
        //{
        //    yield return this;
        //    if (var directSubcategory in Subcategories)
        //        foreach (var subcategory in directSubcategory.AllSubcategories())
        //        {
        //            yield return subcategory;
        //        }
        //}


    }
}

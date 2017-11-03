using System;
using System.Collections.Generic;
using System.Text;
using SIG.Data.Entity;
using SIG.Data.Entity.Identity;

namespace SIG.Data.Entity.Identity
{
    public class MenuCategory : IAuditedEntity
    {
        public int Id { get; set; }
        public MenuCategory()
        {
            this.Menus = new HashSet<Menu>();
        }

        public string Title { get; set; }
        public int Importance { get; set; }
        public bool IsSys { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SIG.Data.Entity
{
    public partial class GoodsCategory : IAuditedEntity
    {
        public GoodsCategory()
        {
         
            //this.GoodAttributes = new HashSet<GoodsAttribute>();
            this.Goodses = new HashSet<Goods>();
            //this.GoodsSpecialPropertys = new HashSet<GoodsSpecialProperty>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SeoName { get; set; }
        public string Thumbnail { get; set; }
        public int Importance { get; set; }
        public bool Active { get; set; }
        public bool Recommend { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        //public virtual ICollection<GoodsAttribute> GoodAttributes { get; set; }
        public virtual ICollection<Goods> Goodses { get; set; }
        //public virtual ICollection<GoodsSpecialProperty> GoodsSpecialPropertys { get; set; }
    }
}

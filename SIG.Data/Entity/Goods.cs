using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIG.Data.Entity
{
    public partial class Goods:IAuditedEntity
    {
        //public Goods()
        //{
        //    this.EntityName = EntityNames.Goods;
        //    //this.GoodsSpecialPropertyValues = new HashSet<GoodsSpecialPropertyValue>();
        //    this.GoodsPhotos = new HashSet<GoodsPhoto>();
        //}
        public int Id { get; set; }
        [ForeignKey("GoodsCategory")]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string Thumbnail { get; set; }
        public int ViewCount { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual GoodsCategory GoodsCategory { get; set; }
        //public virtual ICollection<GoodsSpecialPropertyValue> GoodsSpecialPropertyValues { get; set; }  
       
        //public virtual ICollection<GoodsPhoto> GoodsPhotos { get; set; }
      
        //public ICollection<OrderDetail> OrderDetails { get; set; }
       
        //public ICollection<Cart> Carts { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIG.Data.Mapping
{
    public class GoodsCategoryMap : IEntityTypeConfiguration<GoodsCategory>
    {
        public void Configure(EntityTypeBuilder<GoodsCategory> builder)
        {
            builder.HasKey(b => b.Id);
            builder.ToTable("GoodsCategorySet");
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Importance).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);
        }
    }
}

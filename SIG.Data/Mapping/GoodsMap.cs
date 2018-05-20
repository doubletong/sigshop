using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIG.Data.Mapping
{
    public class GoodsMap : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder)
        {
            builder.HasKey(b => b.Id);
            builder.ToTable("GoodsSet");

            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);

            builder.HasOne(p => p.GoodsCategory)
              .WithMany(h => h.Goodses)
              .HasForeignKey(p => p.CategoryId);
        }
    }
}
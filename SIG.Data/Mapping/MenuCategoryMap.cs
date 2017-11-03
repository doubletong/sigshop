using SIG.Data.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIG.Data.Mapping
{
    public class MenuCategoryMap : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder.HasKey(b => b.Id);
            builder.ToTable("MenuCategorySet");
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Importance).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);
        }
    }
    
}

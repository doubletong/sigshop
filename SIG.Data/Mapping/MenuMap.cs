using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity.Identity;

namespace SIG.Data.Mapping
{

    public class MenuMap : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(b => b.Id);
            builder.ToTable("MenuSet");
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();

            builder.Property(p => p.Importance).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.IsExpand).IsRequired();
            builder.Property(p => p.LayoutLevel);
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);

            builder.HasOne(c => c.ParentMenu)
                .WithMany(c => c.ChildMenus)
                .HasForeignKey(c => c.ParentId);

            builder.HasOne(p => p.Category)
                .WithMany(h => h.Menus)
                .HasForeignKey(p => p.CategoryId);


        }
    }
    
}

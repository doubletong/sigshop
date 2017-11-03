using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity.Identity;

namespace SIG.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
       

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);
            builder.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            builder.ToTable("RoleSet");

           
        }
    }
}
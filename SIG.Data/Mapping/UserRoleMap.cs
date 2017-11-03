using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity.Identity;

namespace SIG.Data.Mapping
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(pc => new { pc.UserId, pc.RoleId });

            builder.HasOne(pc => pc.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pc => pc.UserId);

            builder.HasOne(pc => pc.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(pc => pc.RoleId);

            builder.ToTable("UserRoleSet");
           

        }


    }
}

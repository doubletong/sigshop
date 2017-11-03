using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity.Identity;

namespace SIG.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
 
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.RealName)
                .HasMaxLength(50);

            builder.Property(e => e.UserName).IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Email).IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.QQ)
              
                .HasMaxLength(50);

            // Table & Column Mappings
            builder.ToTable("UserSet");

         
           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity.Identity;

namespace SIG.Data.Mapping
{
    public class RoleMenuMap : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.HasKey(pc => new { pc.RoleId, pc.MenuId });

            builder.HasOne(pc => pc.Menu)
                .WithMany(p => p.RoleMenus)
                .HasForeignKey(pc => pc.MenuId);

            builder.HasOne(pc => pc.Role)
                .WithMany(c => c.RoleMenus)
                .HasForeignKey(pc => pc.RoleId);

            builder.ToTable("RoleMenuSet");


        }


    }
}

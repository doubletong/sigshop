using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIG.Data.Entity;

namespace SIG.Data.Mapping
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {


        public void Configure(EntityTypeBuilder<Log> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Application)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Logged)
                .IsRequired();
            builder.Property(t => t.Level)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(t => t.Message)
                .IsRequired();

            builder.Property(t => t.Logger)
                .HasMaxLength(250);
            builder.Property(t => t.CreatedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            builder.ToTable("LogSet");


        }
    }
}

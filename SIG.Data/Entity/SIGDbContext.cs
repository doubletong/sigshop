using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SIG.Data.Entity.Identity;
using SIG.Data.Mapping;
using SIG.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore.Metadata;
using SIG.EntityFrameworkCore.AutoHistory.Extensions;

namespace SIG.Data.Entity
{

    public class SIGDbContext : DbContext
    {
        public SIGDbContext(DbContextOptions<SIGDbContext> options) : base(options)
        {
        }

        //public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        //public virtual DbSet<Article> Article { get; set; }
        //public virtual DbSet<Comment> Comment { get; set; }
        //public virtual DbSet<FilterTemplate> FilterTemplate { get; set; }
        //public virtual DbSet<VideoCategory> VideoCategory { get; set; }
        //public virtual DbSet<Video> Video { get; set; }
        //public virtual DbSet<Reservation> Reservation { get; set; }

        //public virtual DbSet<Page> Page { get; set; }
        //public virtual DbSet<PageMeta> PageMeta { get; set; }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuCategory> MenuCategory { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        //public virtual DbSet<Log> Log { get; set; }
        //public virtual DbSet<Email> Emails { get; set; }
        //public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        //public virtual DbSet<EmailAccount> EmailAccounts { get; set; }
        //public virtual DbSet<Position> Positions { get; set; }
        //public virtual DbSet<Carousel> Carousels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserRoleMap());
            builder.ApplyConfiguration(new MenuMap());
            builder.ApplyConfiguration(new MenuCategoryMap());
            builder.ApplyConfiguration(new RoleMenuMap());

            builder.EnableAutoHistory(null);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
     
       

        public override int SaveChanges()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedDate = now;
                added.CreatedBy = Site.CurrentUserName;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.UpdatedDate = now;
                modified.UpdatedBy = Site.CurrentUserName;

                base.Entry(modified).Property(x => x.CreatedBy).IsModified = false;
                base.Entry(modified).Property(x => x.CreatedDate).IsModified = false;

            }

            return base.SaveChanges();
        }


    }
}

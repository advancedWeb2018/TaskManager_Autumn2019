using MakeIt.DAL.Common;
using MakeIt.DAL.ModelInitializer;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Principal;
using static MakeIt.DAL.ModelConfiguration.EFConfiguration;

namespace MakeIt.DAL.EF
{
    public class MakeItContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        private static MakeItContext _instance;
        static MakeItContext()
        {
            Database.SetInitializer(new ContextInitializer());
        }

        public MakeItContext()
            : base("MakeItContext")
        {
            // Lazy loading
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public static MakeItContext Create()
        {
            if (_instance == null)
                _instance = new MakeItContext();

            return _instance;
        }

        // Identity entities
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }

        // Other app entities
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<Milestone> Milestones { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        // Override the OnModelCreating method to add
        // configuration settings
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Identity configurations
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());

            //Other model configurations
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ColorConfiguration());
            modelBuilder.Configurations.Add(new LabelConfiguration());
            modelBuilder.Configurations.Add(new MilestoneConfiguration());
            modelBuilder.Configurations.Add(new PriorityConfiguration());
            modelBuilder.Configurations.Add(new StatusConfiguration());
            modelBuilder.Configurations.Add(new TaskConfiguration());
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
              .Where(x => x.Entity is IAuditableEntity
                  && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    //TODO later for Web
                    string identityName = new WindowsPrincipal(WindowsIdentity.GetCurrent()).Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }
            return base.SaveChanges();
        }
    }
}

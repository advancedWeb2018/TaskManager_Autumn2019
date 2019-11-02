using MakeIt.DAL.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MakeIt.Test
{
    public class TestContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public TestContext()
            : base("Name=TestContext")
        {

        }

        public TestContext(bool enableLazyLoading, bool enableProxyCreation)
            : base("Name=TestContext")
        {
            Configuration.ProxyCreationEnabled = enableProxyCreation;
            Configuration.LazyLoadingEnabled = enableLazyLoading;
        }

        public TestContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        // Identity entities
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        // Other app entities
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check          
            Database.SetInitializer<TestContext>(new AlwaysCreateInitializer());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public void Seed(TestContext context)
        {
            var listUsers = new List<User>
            {
                new User {UserName = "User1", PasswordHash="123456", Email="User1@gmail.com"},
                new User {UserName = "User2", PasswordHash="234567", Email="User2@gmail.com"},
                new User {UserName = "User3", PasswordHash="345678", Email="User3@gmail.com"}
            };
            foreach (var user in listUsers)
            {
                context.Users.Add(user);
            }    
            context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }
    }
}

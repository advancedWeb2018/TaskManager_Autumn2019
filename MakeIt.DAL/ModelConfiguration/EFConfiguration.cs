using MakeIt.DAL.EF;
using System.Data.Entity.ModelConfiguration;

namespace MakeIt.DAL.ModelConfiguration
{
    public class EFConfiguration
    {
        public class CommentConfiguration : EntityTypeConfiguration<Comment>
        {
            public CommentConfiguration()
            {
                Property(c => c.Text).IsRequired().HasMaxLength(5000);

                // configures one-to-many relationship
                HasRequired<Task>(c => c.Task)
                    .WithMany(t => t.Comments)
                    .Map(m => m.MapKey("TaskId"));
                ToTable("Comment");
            }
        }

        public class ProjectConfiguration : EntityTypeConfiguration<Project>
        {
            public ProjectConfiguration()
            {
                HasIndex(p => p.Name).IsUnique();
                Property(p => p.Name).IsRequired().HasMaxLength(20);
                ToTable("Project");
            }
        }

        public class ColorConfiguration : EntityTypeConfiguration<Color>
        {
            public ColorConfiguration()
            {
                HasIndex(c => c.Name).IsUnique();
                Property(c => c.Name).IsRequired().HasMaxLength(20);
                HasIndex(c => c.Code).IsUnique();
                Property(c => c.Code).IsRequired().HasMaxLength(7);
                ToTable("Color");
            }
        }

        public class LabelConfiguration : EntityTypeConfiguration<Label>
        {
            public LabelConfiguration()
            {
                Property(c => c.Title).IsRequired().HasMaxLength(50);

                // configures zero or one-to-many relationship
                HasOptional<Color>(l => l.Color)
                    .WithMany(c => c.Labels)
                    .Map(m => m.MapKey("ColorId"));
                ToTable("Label");
            }
        }

        public class MilestoneConfiguration : EntityTypeConfiguration<Milestone>
        {
            public MilestoneConfiguration()
            {
                Property(m => m.Title).IsRequired().HasMaxLength(50);
                ToTable("Milestone");
            }
        }

        public class PriorityConfiguration : EntityTypeConfiguration<Priority>
        {
            public PriorityConfiguration()
            {
                HasIndex(p => p.Name).IsUnique();
                Property(p => p.Name).IsRequired().HasMaxLength(20);
                ToTable("Priority");
            }
        }

        public class StatusConfiguration : EntityTypeConfiguration<Status>
        {
            public StatusConfiguration()
            {
                HasIndex(s => s.Name).IsUnique();
                Property(s => s.Name).IsRequired().HasMaxLength(20);
                ToTable("Status");
            }
        }

        public class TaskConfiguration : EntityTypeConfiguration<Task>
        {
            public TaskConfiguration()
            {
                Property(t => t.Title).IsRequired().HasMaxLength(100);
                Property(t => t.Description).HasMaxLength(500);

                // configures many-to-many relationship
                HasMany<Label>(t => t.Labels)
               .WithMany(l => l.Tasks)
               .Map(tl =>
               {
                   tl.MapLeftKey("TaskId");
                   tl.MapRightKey("LabelId");
                   tl.ToTable("TaskLabel");
               });

                // configures one-to-many relationship 
                HasRequired<Project>(t => t.Project)
                    .WithMany(p => p.Tasks)
                    .Map(m => m.MapKey("ProjectId"));

                HasRequired<Priority>(t => t.Priority)
                    .WithMany(p => p.Tasks)
                    .Map(m => m.MapKey("PriorityId"));

                HasRequired<Status>(t => t.Status)
                    .WithMany(p => p.Tasks)
                    .Map(m => m.MapKey("StatusId"));

                HasRequired<User>(t => t.CreatedUser)
                    .WithMany(u => u.CreatedTasks)
                    .Map(m => m.MapKey("CreatedUserId"));

                // configures zero or one-to-many relationship
                HasOptional<Milestone>(t => t.Milestone)
                    .WithMany(m => m.Tasks)
                    .Map(m => m.MapKey("MilestoneId"));

                HasOptional<User>(t => t.AssignedUser)
                     .WithMany(u => u.AssignedTasks)
                     .Map(m => m.MapKey("AssignedUserId"));

                ToTable("Task");
            }
        }

        public class UserConfiguration : EntityTypeConfiguration<User>
        {
            public UserConfiguration()
            {
                HasKey(u => u.Id);
                Property(u => u.PasswordHash).HasMaxLength(500);
                Property(u => u.SecurityStamp).HasMaxLength(500);
                Property(u => u.PhoneNumber).HasMaxLength(50);
                ToTable("User");
            }
        }

        public class RoleConfiguration : EntityTypeConfiguration<Role>
        {
            public RoleConfiguration()
            {
                HasKey(r => r.Id);
                ToTable("Role");
            }
        }

        public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
        {
            public UserRoleConfiguration()
            {
                HasKey(ur => new { ur.UserId, ur.RoleId });
                ToTable("UserRole");
            }
        }

        public class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
        {
            public UserLoginConfiguration()
            {
                HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
                ToTable("UserLogin");
            }
        }

        public class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
        {
            public UserClaimConfiguration()
            {
                Property(u => u.ClaimType).HasMaxLength(150);
                Property(u => u.ClaimValue).HasMaxLength(500);
                ToTable("UserClaim");
            }
        }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            this.CreatedTasks = new HashSet<Task>();
            this.AssignedTasks = new HashSet<Task>();
        }
        // Additional fields
        public bool IsFired { get; set; }

        public virtual ICollection<Task> CreatedTasks { get; set; }
        public virtual ICollection<Task> AssignedTasks { get; set; }
    }
}

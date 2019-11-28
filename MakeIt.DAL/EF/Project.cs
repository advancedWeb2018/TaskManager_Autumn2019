using MakeIt.DAL.Common;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class Project : AuditableEntity<int>
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
            Members = new HashSet<User>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsClosed { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<User> Members { get; set; }
    }
}

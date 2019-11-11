using MakeIt.DAL.Common;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class Priority : AuditableEntity<int>
    {
        public Priority()
        {
            this.Tasks = new HashSet<Task>();
        }
        public string Name { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

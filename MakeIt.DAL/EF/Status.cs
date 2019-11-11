using MakeIt.DAL.Common;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class Status: AuditableEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
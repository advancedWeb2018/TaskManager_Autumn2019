using MakeIt.DAL.Common;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class Label : AuditableEntity<int>
    {
        public Label()
        {
            this.Tasks = new HashSet<Task>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

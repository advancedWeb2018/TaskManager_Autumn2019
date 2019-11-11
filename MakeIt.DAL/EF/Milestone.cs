using MakeIt.DAL.Common;
using System;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class Milestone : AuditableEntity<int>
    {
        public Milestone()
        {
            this.Tasks = new HashSet<Task>();
        }   
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

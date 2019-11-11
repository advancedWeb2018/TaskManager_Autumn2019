using MakeIt.DAL.Common;
using System;
using System.Collections.Generic;
namespace MakeIt.DAL.EF
{
    public class Task: AuditableEntity<int>
    {
        public Task()
        {
            this.Labels = new HashSet<Label>();
            this.Comments = new HashSet<Comment>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public virtual Milestone Milestone { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Status Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual User CreatedUser { get; set; }
        public virtual User AssignedUser { get; set; }
        public virtual ICollection<Label> Labels { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
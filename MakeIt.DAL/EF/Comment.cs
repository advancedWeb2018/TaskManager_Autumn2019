using MakeIt.DAL.Common;
using System;

namespace MakeIt.DAL.EF
{
    public class Comment : AuditableEntity<int>
    {
        public string Text { get; set; }
        public DateTime WrittenDate { get; set; }
        public virtual User User { get; set; }
        public virtual Task Task { get; set; }
    }
}

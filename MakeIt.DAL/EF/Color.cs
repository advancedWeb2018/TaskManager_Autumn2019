using MakeIt.DAL.Common;
using System.Collections.Generic;

namespace MakeIt.DAL.EF
{
    public class Color : AuditableEntity<int>
    {
        public Color()
        {
            this.Labels = new HashSet<Label>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Label> Labels { get; set; }
    }
}

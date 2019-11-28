using MakeIt.BLL.Enum;
using System;

namespace MakeIt.BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsClosed { get; set; }
        public RoleInProjectEnum RoleInProject { get; set; }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MakeIt.DAL.EF
{
    public class Role : IdentityRole<int, UserRole>
    {
        // Additional fields
        public DateTime CreateDateTime { get; set; }
        public DateTime EditDateTime { get; set; }
    }
}

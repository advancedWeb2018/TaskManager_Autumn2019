﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MakeIt.DAL.EF
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            CreatedProjects = new HashSet<Project>();
            RelatedProjects = new HashSet<Project>();
            CreatedTasks = new HashSet<Task>();
            AssignedTasks = new HashSet<Task>();
        }
        // Additional fields
        public bool IsFired { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime EditDateTime { get; set; }

        public virtual ICollection<Project> RelatedProjects { get; set; }
        public virtual ICollection<Project> CreatedProjects { get; set; }
        public virtual ICollection<Task> CreatedTasks { get; set; }
        public virtual ICollection<Task> AssignedTasks { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}

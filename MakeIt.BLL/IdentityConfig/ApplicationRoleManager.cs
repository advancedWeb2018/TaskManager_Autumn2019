using MakeIt.DAL.EF;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace MakeIt.BLL.IdentityConfig
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(IRoleStore<Role, int> store) : base(store) { }

        public override Task<IdentityResult> CreateAsync(Role role)
        {
            role.CreateDateTime = DateTime.UtcNow;
            return base.CreateAsync(role);
        }

        public override Task<IdentityResult> UpdateAsync(Role role)
        {
            role.EditDateTime = DateTime.UtcNow;
            return base.UpdateAsync(role);
        }
    }
}

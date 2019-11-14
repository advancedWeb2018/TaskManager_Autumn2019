using MakeIt.DAL.EF;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MakeIt.BLL.Identity
{
    public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(MakeItContext db) : base(db)
        {

        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(MakeItContext context) : base(context)
        {
        }
    }
}

using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using MakeIt.Repository.Repository.Interface;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {

        }
    }
}

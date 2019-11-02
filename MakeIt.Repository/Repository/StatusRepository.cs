using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IStatusRepository : IGenericRepository<Status>
    {
    }
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(DbContext context)
            : base(context)
        {

        }
    }
}

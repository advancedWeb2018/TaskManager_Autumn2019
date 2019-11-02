using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IMilestoneRepository : IGenericRepository<Milestone>
    {
    }
    public class MilestoneRepository : GenericRepository<Milestone>, IMilestoneRepository
    {
        public MilestoneRepository(DbContext context)
            : base(context)
        {

        }
    }
}

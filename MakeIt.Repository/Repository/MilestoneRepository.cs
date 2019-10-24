using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using MakeIt.Repository.Repository.Interface;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public class MilestoneRepository : BaseRepository<Milestone>, IMilestoneRepository
    {
        public MilestoneRepository(DbContext context)
            : base(context)
        {

        }
    }
}

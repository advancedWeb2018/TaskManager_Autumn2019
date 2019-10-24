using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using MakeIt.Repository.Repository.Interface;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context)
            : base(context)
        {

        }
    }
}

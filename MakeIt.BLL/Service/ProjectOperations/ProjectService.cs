using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeIt.BLL.Service.ProjectOperations
{
    public interface IProjectService : IEntityService<Project>
    {
        IEnumerable<ProjectDTO> GetUserProjectsById(int userId);
    }

    public class ProjectService : EntityService<Project>, IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<ProjectDTO> GetUserProjectsById(int userId)
        {
            var projectList = _unitOfWork.GetRepository<Project>().Find(p => p.Owner.Id == userId);
            return _mapper.Map<IEnumerable<ProjectDTO>>(projectList);
        }
    }
}

using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;
using System.Collections.Generic;

namespace MakeIt.BLL.Service.ProjectOperations
{
    public interface IProjectService : IEntityService<Project>
    {
        IEnumerable<ProjectDTO> GetUserProjectsById(int userId);
        ProjectDTO CreateProject(ProjectDTO project, int ownerId);
    }

    public class ProjectService : EntityService<Project>, IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
        }

        public ProjectDTO CreateProject(ProjectDTO project, int ownerId)
        {
            var projectAdded = new Project
            {
                Name = project.Name,
                Description = project.Description,
                Owner = _unitOfWork.GetRepository<User>().Get(ownerId)
            };
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Project>().Add(projectAdded);
                _unitOfWork.SaveChanges();
            };
            return _mapper.Map<ProjectDTO>(projectAdded);
        }

        public IEnumerable<ProjectDTO> GetUserProjectsById(int userId)
        {
            var projectList = _unitOfWork.GetRepository<Project>().Find(p => p.Owner.Id == userId);
            return _mapper.Map<IEnumerable<ProjectDTO>>(projectList);
        }
    }
}

using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Enum;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace MakeIt.BLL.Service.ProjectOperations
{
    public interface IProjectService : IEntityService<Project>
    {
        IEnumerable<ProjectDTO> GetUserProjectsById(int userId);
        void CreateProject(ProjectDTO project, int ownerId);
        void EditProject(ProjectDTO project);
    }

    public class ProjectService : EntityService<Project>, IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
        }

        public void CreateProject(ProjectDTO project, int ownerId)
        {
            var projectAdded = new Project
            {
                Name = project.Name,
                Description = project.Description,
                Owner = _unitOfWork.GetRepository<User>().Get(ownerId)
            };
            _unitOfWork.GetRepository<Project>().Add(projectAdded);
            _unitOfWork.SaveChanges();
        }

        public void EditProject(ProjectDTO project)
        {
            var projectEdited = _unitOfWork.GetRepository<Project>().Get(project.Id);
            projectEdited.Name = project.Name;
            projectEdited.Description = project.Description;
            _unitOfWork.GetRepository<Project>().Edit(projectEdited);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<ProjectDTO> GetUserProjectsById(int userId)
        {
            // Owner projects
            var projectOwnerList = _unitOfWork.GetRepository<Project>()
                .Find(p => p.Owner.Id == userId);
            var projectOwnerDTOList = _mapper.Map<IEnumerable<ProjectDTO>>(projectOwnerList);
            projectOwnerDTOList.ToList().ForEach(p => p.RoleInProject = RoleInProjectEnum.Owner);

            // Member projects
            var projectMemberList = _unitOfWork.GetRepository<Project>()
                .Find(p=>p.Members.Where(m => m.Id == userId).Any());
            var projectMemberDTOList = _mapper.Map<IEnumerable<ProjectDTO>>(projectMemberList);
            projectMemberDTOList.ToList().ForEach(p => p.RoleInProject = RoleInProjectEnum.Member);

            return projectOwnerDTOList.Union(projectMemberDTOList);
        }
    }
}

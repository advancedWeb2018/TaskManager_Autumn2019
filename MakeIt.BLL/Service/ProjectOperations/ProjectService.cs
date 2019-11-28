﻿using AutoMapper;
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
        ProjectDTO GetProjectById(int projectId);
        ProjectDTO CreateProject(ProjectDTO project, int ownerId);
        ProjectDTO EditProject(ProjectDTO project);
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
            _unitOfWork.GetRepository<Project>().Add(projectAdded);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectDTO>(projectAdded);
        }

        public ProjectDTO EditProject(ProjectDTO project)
        {
            var projectEdited = _unitOfWork.GetRepository<Project>().Get(project.Id);
            projectEdited.Name = project.Name;
            projectEdited.Description = project.Description;
            _unitOfWork.GetRepository<Project>().Edit(projectEdited);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectDTO>(projectEdited);
        }

        public ProjectDTO GetProjectById(int projectId)
        {
            var project = _unitOfWork.GetRepository<Project>().Get(projectId);
            return _mapper.Map<ProjectDTO>(project);
        }

        public IEnumerable<ProjectDTO> GetUserProjectsById(int userId)
        {
            // Owner projects
            var projectOwnerList = _unitOfWork.GetRepository<Project>()
                .Find(p => p.Owner.Id == userId).ToList();
            var projectOwnerDTOList = _mapper.Map<IEnumerable<ProjectDTO>>(projectOwnerList);
            projectOwnerDTOList.ToList().ForEach(p => p.RoleInProject = RoleInProjectEnum.Owner);

            // Member projects
            var projectMemberList = _unitOfWork.GetRepository<Project>()
                .Find(p=>p.Members.Where(m => m.Id == userId).Any()).ToList();
            var projectMemberDTOList = _mapper.Map<IEnumerable<ProjectDTO>>(projectMemberList);
            projectMemberDTOList.ToList().ForEach(p => p.RoleInProject = RoleInProjectEnum.Member);

            return projectOwnerDTOList.Union(projectMemberDTOList);
        }
    }
}

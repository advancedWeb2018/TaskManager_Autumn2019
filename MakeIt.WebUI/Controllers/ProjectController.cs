﻿using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.BLL.Service.ProjectOperations;
using MakeIt.WebUI.Filters;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [AuthenticationFilter]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IAuthorizationService _authorizationService;
        public ProjectController(IMapper mapper, IProjectService projectService, IAuthorizationService authorizationService) : base(mapper)
        {
            _projectService = projectService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();
            var projectDTOList = _projectService.GetUserProjectsById(userId).ToList();
            var projectViewModelList = _mapper.Map<IEnumerable<ProjectViewModel>>(projectDTOList);
            return View(projectViewModelList);
        }

        [HttpGet]
        public ActionResult Edit(int? projectId, bool isNewProject = false)
        {
            if (TempData["Message"]!=null)
            {
                ViewBag.SuccessMsg = TempData["Message"];
            }

            int userId = User.Identity.GetUserId<int>();
            ViewBag.ActionDetermination = isNewProject ? "Create" : "Edit";
            if (isNewProject)
            {
                return View(new ProjectViewModel { RoleInProject = BLL.Enum.RoleInProjectEnum.Owner});
            }
            var projectDTO = _projectService.GetProjectById(projectId.Value);
            projectDTO.RoleInProject = projectDTO.Owner.Id == userId ?
                                        BLL.Enum.RoleInProjectEnum.Owner :
                                        BLL.Enum.RoleInProjectEnum.Member;

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectDTO);
            return View(projectViewModel);
        }

        [HttpPost]
        public ActionResult EditProject(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            ViewBag.ActionResult = model.Id == null ? "Just created new" : "Just edited";
            ViewBag.ActionDetermination = "Edit";

            int userId = User.Identity.GetUserId<int>();
            var projectDTO = _mapper.Map<ProjectDTO>(model);

            var projectEditedDTO = new ProjectDTO();     
            if (model.Id == null)
                projectEditedDTO = _projectService.CreateProject(projectDTO, userId);
            else projectEditedDTO = _projectService.EditProject(projectDTO);

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectEditedDTO);
            
            return View("Edit", projectViewModel);
        }

        public async Task<ActionResult> ConfirmInvite(int userId, int projectId, string code)
        {
            var userDTO = await _authorizationService.FindByIdAsync(userId.ToString());
            if (_authorizationService.IsTokenExpired(userDTO, code))
            {
                return View("CrashedLink");
            }
            var projectViewModel = _mapper.Map<ProjectViewModel>(_projectService.GetProjectById(projectId));
            ViewBag.ActionDetermination = "Edit";
            if (_authorizationService.IsProjectMember(userId, projectId))
            {
                ViewBag.ActionResult = "You are already a member of this project";
                return View("Edit", projectViewModel);
            }
            var projectAddedViewModel = _mapper.Map<ProjectViewModel>(_projectService.AddProjectMember(userId, projectId));
            ViewBag.ActionResult = "You have joined to project just now";
            return View("Edit", projectAddedViewModel);
        }
    }
}
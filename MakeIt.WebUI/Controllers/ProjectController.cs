using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.ProjectOperations;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        public ProjectController(IMapper mapper, IProjectService projectService) : base(mapper)
        {
            _projectService = projectService;
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
        public ActionResult Edit(int? projectId, bool isNewProject)
        {
            ViewBag.ActionDetermination = isNewProject ? "Create new" : "Edit";
            if (isNewProject)
            {
                return View();
            }
            var projectDTO = _projectService.GetProjectById(projectId.Value);
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
            int userId = User.Identity.GetUserId<int>();
            var projectDTO = _mapper.Map<ProjectDTO>(model);

            var projectEditedDTO = new ProjectDTO();

            ViewBag.ActionDetermination = model.Id == null ? "Created new" : "Edited";

            if (model.Id == null)
                projectEditedDTO = _projectService.CreateProject(projectDTO, userId);

            else projectEditedDTO = _projectService.EditProject(projectDTO);

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectEditedDTO);
            
            return View("Edit", projectViewModel);
        }
    }
}
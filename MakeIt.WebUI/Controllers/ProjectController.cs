using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.ProjectOperations;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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
            var projectDTOList = _projectService.GetUserProjectsById(userId);
            var projectViewModelList = _mapper.Map<IEnumerable<ProjectViewModel>>(projectDTOList);
            return View(projectViewModelList);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int userId = User.Identity.GetUserId<int>();
            var projectDTO = _mapper.Map<ProjectDTO>(model);
            if (model.Id == null)
            {
                var projectDTOAdded = _projectService.CreateProject(projectDTO, userId);
                return RedirectToAction("Index", "Project");
            }
            return View();
        }
    }
}
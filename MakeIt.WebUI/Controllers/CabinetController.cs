using AutoMapper;
using MakeIt.Repository.UnitOfWork;
using MakeIt.WebUI.ViewModel.Task;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    public class CabinetController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public CabinetController(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userCurrentTasks = _unitOfWork.Tasks.GetAll().ToList();
            //var userOldTasks = _unitOfWork.Tasks.GetAll();

            List<TaskShowViewModel> taskDisplay = new List<TaskShowViewModel>();
            foreach (var test in userCurrentTasks)
            {
                TaskShowViewModel task = new TaskShowViewModel();
                task.AssignedUser = test.AssignedUser.UserName.ToString();
                task.Title = test.Title;
                task.Priority = test.Priority.Name;
                task.Project = test.Project.Name;
                task.Milestone = test.Milestone.Title;
                taskDisplay.Add(task);
            }
            ViewBag.CurrentTask = taskDisplay;
            //ViewBag.OldTask = userOldTasks;
            ViewBag.User = User.Identity.Name;
            return View();
        }
    }
}
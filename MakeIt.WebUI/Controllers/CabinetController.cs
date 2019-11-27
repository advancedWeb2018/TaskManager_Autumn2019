using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.TaskOperations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;


namespace MakeIt.WebUI.Controllers
{
    [Authorize]
    public class CabinetController : BaseController
    {
        //private IUnitOfWork _unitOfWork;
        private readonly ITaskService _tasktService;

        public CabinetController(IMapper mapper, ITaskService taskService) : base(mapper)
        {
            //_unitOfWork = unitOfWork;
            _tasktService = taskService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();
            var taskDTOList = _tasktService.GetUserTasksById(userId);
            var taskViewModelList = _mapper.Map<IEnumerable<TaskShowViewModel>>(taskDTOList);
            /*var currentUser = User.Identity.Name;
            var currentUserID = _unitOfWork.Users.GetAll().First(user => user.UserName.ToUpper().Equals(currentUser.ToUpper())).Id;
            var userCurrentTasks = _unitOfWork.Tasks.GetAll().ToList();//.Where(task => task.AssignedUser.Equals(currentUserID)).ToList();
            //var userOldTasks = _unitOfWork.Tasks.GetAll();

            var taskDisplay = new List<TaskShowViewModel>();
            foreach (var test in userCurrentTasks)
            {
                TaskShowViewModel task = new TaskShowViewModel();                
                task.Title = test.Title;
                task.Priority = test.Priority.Name;
                task.Project = test.Project.Name;
                if (test.Milestone != null)
                    task.Milestone = test.Milestone.Title;
                else
                    task.Milestone = "";
                taskDisplay.Add(task);
            }
            ViewBag.CurrentTask = taskDisplay;
            //ViewBag.OldTask = userOldTasks;
            ViewBag.User = User.Identity.Name;*/
            return View(taskViewModelList);
        }
    }
}
using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.TaskOperations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;


namespace MakeIt.WebUI.Controllers
{
    [Authorize]
    public class CabinetController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly ITaskService _tasktService;

        public CabinetController(IMapper mapper, ITaskService taskService, IUnitOfWork unitOfWork) : base(mapper)
        {
            _unitOfWork = unitOfWork;
            _tasktService = taskService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();
            //var taskDTOList = _tasktService.GetUserTasksById(userId);
            var ttasks = _unitOfWork.Tasks.GetAll().ToList();
            var tasks = ttasks.Where(t => t.AssignedUser.Id == userId);
            //var taskViewModelList = _mapper.Map<IEnumerable<TaskShowViewModel>>(tasks);
            /*var currentUser = User.Identity.Name;
            var currentUserID = _unitOfWork.Users.GetAll().First(user => user.UserName.ToUpper().Equals(currentUser.ToUpper())).Id;
            var userCurrentTasks = _unitOfWork.Tasks.GetAll().ToList();//.Where(task => task.AssignedUser.Equals(currentUserID)).ToList();
            //var userOldTasks = _unitOfWork.Tasks.GetAll();*/

            var taskViewModelList = new List<TaskShowViewModel>();
            foreach (var test in tasks)
            {
                TaskShowViewModel task = new TaskShowViewModel();
                task.Id = test.Id;
                task.Title = test.Title;
                task.Description = test.Description;
                task.Priority = test.Priority.Name;
                task.Project = test.Project.Name;
                task.Status = test.Status.Name;
                task.DueDate = test.DueDate;
                task.AssignedUser = test.AssignedUser.UserName;

                taskViewModelList.Add(task);
            }
            //ViewBag.CurrentTask = taskViewModelList;
            //ViewBag.OldTask = userOldTasks;
            //ViewBag.User = User.Identity.Name;
            return View(taskViewModelList);
        }
    }
}
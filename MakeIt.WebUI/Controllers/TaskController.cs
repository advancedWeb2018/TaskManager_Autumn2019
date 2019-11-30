using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakeIt.Repository.UnitOfWork;
using MakeIt.WebUI.ViewModel;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.TaskOperations;
using Microsoft.AspNet.Identity;
using AutoMapper;
using MakeIt.WebUI.Filters;

namespace MakeIt.WebUI.Controllers
{
    [AuthenticationFilter]
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        private UnitOfWork unitOfWork;
        public TaskController(IMapper mapper, ITaskService  taskService) : base(mapper)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            string currentUser = "";
            if (User.Identity.IsAuthenticated)
            {
                currentUser = User.Identity.Name;
            }
            else 
            {
                return RedirectToAction("LogOff", "Account");
            }
            unitOfWork = new UnitOfWork();
            var currentUserobj = unitOfWork.Users.GetAll()
                .First(us => us.UserName.ToUpper().Equals(currentUser.ToUpper()));

            var projects = unitOfWork.Projects.GetAll().ToList().Where(pr=>pr.Owner == currentUserobj || pr.Members.Contains(currentUserobj));
            var projNames = from proj in projects select proj.Name;
            SelectList project = new SelectList(projNames, "Project");

            ViewBag.Project = project;

           
            var prior = unitOfWork.Priorities.GetAll();
            var prName = from p in prior select p.Name;
            SelectList priority = new SelectList(prName, "Priority");
            ViewBag.Priority = priority;

            var stone = unitOfWork.Milestones.GetAll();
            var stoneNames = from s in stone select s.Title;
            SelectList milestones = new SelectList(stoneNames, "Milestone");
            ViewBag.Milestone = milestones;

            var tu = unitOfWork.Users.GetAll().ToList();


            var users = unitOfWork.Users.GetAll();
            var userNames = from u in users select u.UserName;
            SelectList usersNames= new SelectList(userNames, "AssignedUser");
            ViewBag.Users = usersNames;

            ViewBag.User = currentUser;
            return View();
        }

        [HttpPost]
        public ActionResult Save(TaskShowViewModel newTask)
        {
            var userId = User.Identity.GetUserId<int>();
            var userName = User.Identity.Name;
            int taskId = newTask.Id ?? default(int);

            unitOfWork = new UnitOfWork();
            var oldTask = unitOfWork.Tasks.Get(taskId);

            var model = new TaskShowViewModel();
            if (newTask.Id == null)
            {
                model = new TaskShowViewModel
                {
                    Title = newTask.Title,
                    Description = newTask.Description,
                    Priority = newTask.Priority,
                    Status = "Open",
                    Project = newTask.Project,
                    AssignedUser = newTask.AssignedUser,
                    CreatedUser = userName,
                    DueDate = newTask.DueDate
                };
            }
            else
            {
                model.Id = newTask.Id;
                model.Title = newTask.Title.Equals(oldTask.Title) ? oldTask.Title : newTask.Title;
                model.Description = newTask.Description.Equals(oldTask.Description) ? oldTask.Description : newTask.Description;
                model.Status = newTask.Status != null && !newTask.Status.Equals(oldTask.Status.Name) ?
                    newTask.Status : oldTask.Status.Name;
                model.Priority = newTask.Priority != null && !newTask.Priority.Equals(oldTask.Priority.Name)  ?
                    newTask.Priority: oldTask.Priority.Name;
                model.Project = newTask.Project != null && !newTask.Project.Equals(oldTask.Project.Name) ?
                    newTask.Project : oldTask.Project.Name;
                model.AssignedUser = newTask.AssignedUser != null && newTask.AssignedUser.Equals(oldTask.AssignedUser.UserName) ?
                    newTask.AssignedUser : oldTask.AssignedUser.UserName;
                model.CreatedUser = oldTask.CreatedUser.UserName;
                model.DueDate = newTask.DueDate != null && newTask.DueDate > DateTime.MinValue && newTask.DueDate.ToString("mm/dd/yyyy").Equals(oldTask.DueDate.ToString("mm/dd/yyyy")) ?
                    newTask.DueDate : oldTask.DueDate;
                
            }
            var taskDTO = _mapper.Map<TaskDTO>(model);
            if (model.Id == null)
                taskDTO =_taskService.CreateTask(taskDTO, userId);
            else taskDTO =_taskService.EditTask(taskDTO);                                 

            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public ActionResult Show(int taskId)
        {
            unitOfWork = new UnitOfWork();
            var task = unitOfWork.Tasks.Get(taskId);
            TaskShowViewModel currentTask = new TaskShowViewModel();

            currentTask.Id = task.Id;
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;
            currentTask.Priority = task.Priority.Name;
            currentTask.Status = task.Status.Name;
            currentTask.Project = task.Project.Name;
            currentTask.AssignedUser = task.AssignedUser.UserName;
            currentTask.CreatedUser = task.CreatedUser.UserName;
            currentTask.DueDate = task.DueDate;
            
            return View(currentTask);
        }

        [HttpGet]
        public ActionResult Edit(int taskId)
        {
            unitOfWork = new UnitOfWork();
            var task = unitOfWork.Tasks.Get(taskId);

            TaskShowViewModel currentTask = new TaskShowViewModel();
            currentTask.Id = task.Id;
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;
            currentTask.Priority = task.Priority.Name;
            currentTask.Project = task.Project.Name;
            currentTask.Status = task.Status.Name;
            currentTask.DueDate = task.DueDate;
            currentTask.AssignedUser = task.AssignedUser.UserName;
            currentTask.CreatedUser = task.CreatedUser.UserName;

            string currentUser = User.Identity.Name;
            var currentUserobj = unitOfWork.Users.GetAll()
                .First(us => us.UserName.ToUpper().Equals(currentUser.ToUpper()));

            var projects = unitOfWork.Projects.GetAll().ToList().Where(pr => pr.Owner == currentUserobj || pr.Members.Contains(currentUserobj));
            var projNames = from proj in projects select proj.Name;
            ViewBag.Project = new SelectList(projNames, "Project");

            
            var priority = new SelectList(unitOfWork.Priorities.GetAll(), "Name", "Name");           
            ViewBag.Priority = priority;

            var statuses = new SelectList(unitOfWork.Statuses.GetAll(), "Name", "Name");
            ViewBag.Status = statuses;

            var usersForAssignee = new SelectList(unitOfWork.Users.GetAll(), "UserName", "UserName");
            ViewBag.UsersForAssignee = usersForAssignee;
            
            return View(currentTask);
        }
    }
}
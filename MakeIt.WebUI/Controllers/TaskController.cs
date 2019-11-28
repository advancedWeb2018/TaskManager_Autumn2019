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

namespace MakeIt.WebUI.Controllers
{
    [Authorize]
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
            
            var projects = unitOfWork.Projects.GetAll();
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


            var users = unitOfWork.Users.GetAll();//.ToList();
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
            unitOfWork = new UnitOfWork();
            var model = new TaskShowViewModel
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

            var taskDTO = _mapper.Map<TaskDTO>(model);
            if (model.Id == null)
                _taskService.CreateTask(taskDTO, userId);
            else _taskService.EditTask(taskDTO);
            
            return RedirectToAction("Index", "Cabinet");
        }

        public ActionResult Show(TaskShowViewModel task)
        {
            unitOfWork = new UnitOfWork();
            var comments = unitOfWork.Comments.GetAll().Where(c => c.Task.Id == task.Id);
            ViewBag.Comments = comments;
            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(TaskShowViewModel task)
        {
            unitOfWork = new UnitOfWork();

            SelectList projects = new SelectList(unitOfWork.Projects.GetAll(), "Id", "Name");
            ViewBag.Project = projects;

            SelectList priority = new SelectList(unitOfWork.Priorities.GetAll(), "Id", "Name");
            ViewBag.Priority = priority;

            SelectList statuses = new SelectList(unitOfWork.Statuses.GetAll(), "Id", "Name");
            ViewBag.Status = statuses;

            return View(task);
        }
    }
}
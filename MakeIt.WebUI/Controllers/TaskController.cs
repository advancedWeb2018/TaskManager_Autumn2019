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
            SelectList usersNames= new SelectList(userNames, "Users");
            ViewBag.Users = usersNames;

            ViewBag.User = currentUser;
            return View();
        }

        [HttpPost]
        public ActionResult Save(TaskShowViewModel newTask)
        {
            var userId = User.Identity.GetUserId<int>();
            unitOfWork = new UnitOfWork();
            var tt = unitOfWork.Priorities.GetAll().ToList();
            var ttt = tt.First(t => t.Name == "Low");
            //var milestone = unitOfWork.Milestones.GetAll().First(m => m.Title.ToUpper().Equals(newTask.Milestone.ToUpper()));
            var priority = unitOfWork.Priorities.GetAll().ToList().First(p => p.Name.ToUpper().Equals(newTask.Priority.ToUpper()));
            var project = unitOfWork.Projects.GetAll().ToList().First(pr => pr.Name.ToUpper().Equals(newTask.Project.ToUpper()));
            var model = new TaskShowViewModel
            {
                Title = newTask.Title,
                Description = newTask.Description,
                //Milestone = milestone,
                Priority = newTask.Priority,
                Status = "Open",
                Project = newTask.Project,
                AssignedUser = newTask.AssignedUser,
                /*CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedUser = unitOfWork.Users.Get(userId),*/
                //it's temp behavior
                DueDate = DateTime.Now.AddDays(5)
            };

            /*unitOfWork.Tasks.Add(task);
            unitOfWork.SaveChanges();*/
            var taskDTO = _mapper.Map<TaskDTO>(model);
            if (model.Id == null)
                _taskService.CreateTask(taskDTO, userId);
            else _taskService.EditTask(taskDTO);
            
            return RedirectToAction("Index", "Cabinet");
        }


    }
}
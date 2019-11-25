using AutoMapper;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakeIt.WebUI.ViewModel.Task;
using System.Web.Mvc;
using MakeIt.Repository.UnitOfWork;
using System.Web.Security;
using System.Security.Principal;

namespace MakeIt.WebUI.Controllers
{
    public class CabinetController : BaseController
    {
        private string _user;
        UnitOfWork unitOfWork;

        public CabinetController(IMapper mapper) : base(mapper)
        {
            unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userCurrentTasks = unitOfWork.Tasks.GetAll();
           var userOldTasks = unitOfWork.Tasks.GetAll();

            List<TaskShowViewModel> taskDisplay = new List<TaskShowViewModel>();/*
            foreach(var test in userCurrentTasks)
            {
                TaskShowViewModel task = new TaskShowViewModel();
                task.AssignedUser = test.AssignedUser.UserName.ToString();
                task.Title = test.Title;
                task.Priority = test.Priority.Name;
                task.Project = test.Project.Name;
                task.Milestone = test.Milestone.Title;
                taskDisplay.Add(task);
            }*/
            ViewBag.CurrentTask = taskDisplay;
            //ViewBag.OldTask = userOldTasks;
            ViewBag.User = User.Identity.Name;
            return View();           
        }

    }
}
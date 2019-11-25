using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakeIt.Repository.UnitOfWork;

namespace MakeIt.WebUI.Controllers
{
    public class EditTaskController : Controller
    {
        private UnitOfWork unitOfWork;
        // GET: EditTask
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
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

            ViewBag.User = userName;
            return View();
        }

        [HttpPost]
        public void SaveTask(TaskEventHandler newTask)
        { 


        }


    }
}
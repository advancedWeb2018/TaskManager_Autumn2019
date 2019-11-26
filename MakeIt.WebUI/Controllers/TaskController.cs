using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakeIt.Repository.UnitOfWork;
using MakeIt.WebUI.ViewModel.Task;

namespace MakeIt.WebUI.Controllers
{
    public class TaskController : Controller
    {
        private UnitOfWork unitOfWork;
        // GET: EditTask
       /* public ActionResult Index()
        {

            return View();
        }*/

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
                return View("/Account/LogOff");
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
            unitOfWork = new UnitOfWork();
            var tt = unitOfWork.Priorities.GetAll().ToList();
            var ttt = tt.First(t => t.Name == "Low");
            //var milestone = unitOfWork.Milestones.GetAll().First(m => m.Title.ToUpper().Equals(newTask.Milestone.ToUpper()));
            var priority = unitOfWork.Priorities.GetAll().ToList().First(p => p.Name.ToUpper().Equals(newTask.Priority.ToUpper()));
            var project = unitOfWork.Projects.GetAll().ToList().First(pr => pr.Name.ToUpper().Equals(newTask.Project.ToUpper()));
            DAL.EF.Task task = new DAL.EF.Task
            {
                Title = newTask.Title,
                Description = newTask.Description,
                //Milestone = milestone,
                Priority = priority,
                Status = unitOfWork.Statuses.GetAll().First(s => s.Name.ToUpper().Equals(("Open").ToUpper())),
                Project = project,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                //CreatedUser = unitOfWork.Users.GetAll().First(u => u.UserName.ToUpper().Equals(User.Identity.Name.ToUpper())),
                CreatedUser = unitOfWork.Users.GetAll().First(u => u.UserName.ToUpper().Equals("miron".ToUpper())),
                //it's temp behavior
                DueDate = DateTime.Now.AddDays(5)
            };

            unitOfWork.Tasks.Add(task);
            unitOfWork.SaveChanges();

            return View("~/Cabinet");
        }


    }
}
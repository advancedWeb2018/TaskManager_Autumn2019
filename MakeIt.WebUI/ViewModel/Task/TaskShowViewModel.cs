using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel.Task
{
    public class TaskShowViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Milestone { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Project { get; set; }
        public string AssignedUser { get; set; }
    }
}
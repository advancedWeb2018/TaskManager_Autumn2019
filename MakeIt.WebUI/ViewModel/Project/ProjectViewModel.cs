using System;
using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
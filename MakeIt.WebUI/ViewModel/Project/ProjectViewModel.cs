using System;
using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Maximum Length is 20 characters")]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Maximum Length is 500 characters")]
        [Display(Name = "Description (optional)")]
        public string Description { get; set; }

        [Display(Name = "Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsClosed { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using Persona5API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persona5API.ViewModels
{
    public class SkillsFormViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string SkillName { get; set; }
        public string Effect { get; set; }
        public int Power { get; set; }

        [Display(Name = "Cost")]
        public string SelectedCost { get; set; }
        public IEnumerable<SelectListItem> Cost { get; set; }

        [Required]
        [Display(Name = "Element Name")]
        public string SelectedElement { get; set; }
        public IEnumerable<SelectListItem> Elements { get; set; } 
    }
}
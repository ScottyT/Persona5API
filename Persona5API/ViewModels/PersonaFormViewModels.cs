using Microsoft.AspNetCore.Mvc.Rendering;
using Persona5API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Persona5API.ViewModels
{
    public class PersonaFormViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Arcana { get; set; }
        public PersonaStats Stats { get; set; }
        [Display(Name = "Skills")]
        public List<int> SelectSkillsId { get; set; }
        public IEnumerable<SelectListItem> SkillsList { get; set; }
    }
}

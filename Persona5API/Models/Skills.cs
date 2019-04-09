using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persona5API.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public int LevelLearned { get; set; }
        [Display(Name = "Name")]
        public string SkillName { get; set; }
        public string Effect { get; set; }
        public int Power { get; set; }
        public string CostAmount { get; set; }
        public string ElementName { get; set; }
        public int? CostId { get; set; }
        public int? ElementId { get; set; }
        public int? PersonaId { get; set; }

        public virtual SkillCost Cost { get; set; }
        [Display(Name = "Type")]
        public virtual Elements Element { get; set; }
    }
}

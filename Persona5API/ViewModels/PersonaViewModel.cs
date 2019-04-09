using Persona5API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persona5API.ViewModels
{
    public class PersonaViewModel
    {
        public IEnumerable<Persona> Personas { get; set; }
        public IEnumerable<Skills> PersonaSkills { get; set; }
        public IEnumerable<PersonaStats> PersonaStats { get; set; }
        public IEnumerable<SkillCost> SkillCosts { get; set; }
        public Persona Persona { get; set; }
        public Skills PersonaSkill { get; set; }
    }
}

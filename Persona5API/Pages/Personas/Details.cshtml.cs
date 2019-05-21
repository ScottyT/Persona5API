using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persona5API.Data;
using Persona5API.Models;

namespace Persona5API.Pages.Personas
{
    public class DetailsModel : PageModel
    {
        private readonly Persona5API.Data.ApplicationDbContext _context;

        public DetailsModel(Persona5API.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Persona Persona { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Persona = await _context.Personas.FirstOrDefaultAsync(m => m.Id == id);
            IList<Skills> skills = JsonConvert.DeserializeObject<IList<Skills>>(Persona.SkillsJson);
            var viewPersona = new Persona
            {
                Name = Persona.Name,
                Level = Persona.Level,
                Arcana = Persona.Arcana,
                Skills = skills.ToList(),
                ResistJson = Persona.ResistJson,
                WeakJson = Persona.WeakJson
            };
            if (Persona == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

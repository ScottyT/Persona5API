using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persona5API.Data;
using Persona5API.Models;

namespace Persona5API.Pages.Personas
{
    public class DeleteModel : PageModel
    {
        private readonly Persona5API.Data.ApplicationDbContext _context;

        public DeleteModel(Persona5API.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Persona Persona { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Persona = await _context.Personas.FirstOrDefaultAsync(m => m.Id == id);

            if (Persona == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Persona = await _context.Personas.FindAsync(id);

            if (Persona != null)
            {
                _context.Personas.Remove(Persona);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

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
    public class IndexModel : PageModel
    {
        private readonly Persona5API.Data.ApplicationDbContext _context;

        public IndexModel(Persona5API.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Persona> Persona { get;set; }

        public async Task OnGetAsync()
        {
            Persona = await _context.Personas.Include(x => x.Stats).ToListAsync();
        }
    }
}

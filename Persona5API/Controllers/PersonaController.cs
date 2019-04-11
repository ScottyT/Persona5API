using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persona5API.Data;
using Persona5API.Models;
using Persona5API.ViewModels;

namespace Persona5Api.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Persona
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personas.Include(x => x.Stats).ToListAsync());
        }

        // GET: Persona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Stats)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }
            var viewModel = new PersonaViewModel()
            {
                Persona = persona
            };
            return View(viewModel);
        }

        // GET: Persona/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var viewModel = new PersonaFormViewModels
            {
                SelectSkillsId = new List<int>(),
                SkillsList = await GetSkills(),
                ResistElementsId = new List<int>(),
                ResistList = await GetElements(),
                WeakElementsId = new List<int>(),
                WeakList = await GetElements()
            };
            try
            {
                this.ViewBag.SkillsList = this.GetSkills();
                this.ViewBag.ResistList = this.GetElements();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return View("Create", viewModel);
        }

        // POST: Persona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(PersonaFormViewModels viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View("Create", viewModel);
            }
            var skills = _context.PersonaSkills.Include(s => s.Element).Include(c => c.Cost).ToList();
            var elements = _context.PersonaElements.ToList();
            var persona = new Persona
            {
                Name = viewModel.Name,
                Level = viewModel.Level,
                Arcana = viewModel.Arcana,
                Stats = viewModel.Stats,
                ResistElements = elements.Where(e => viewModel.ResistElementsId.Contains(e.Id)).Select(x => x).ToList(),
                WeakElements = elements.Where(e => viewModel.WeakElementsId.Contains(e.Id)).Select(x => x).ToList(),
                Skills = skills.Where(s => viewModel.SelectSkillsId.Contains(s.Id)).Select(x => x).ToList()
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Persona/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var skills = await GetSkills();
            var elements = await GetElements();
            var persona = _context.Personas.FirstOrDefault(p => p.Id == id);
            var currentSkillsId = persona.Skills.Select(c => c.Id).ToList();
            var viewModel = new PersonaFormViewModels
            {
                Id = persona.Id,
                Name = persona.Name,
                Arcana = persona.Arcana,
                Level = persona.Level,
                Description = persona.Description,
                ResistList = elements,
                WeakList = elements,
                SkillsList = skills,
                SelectSkillsId = currentSkillsId
            };
            if (id == null)
            {
                return NotFound();
            }
           
            return View(viewModel);
        }

        // POST: Persona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(PersonaFormViewModels viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", viewModel);
            }
            var skills = _context.PersonaSkills.Include(s => s.Element).Include(c => c.Cost).ToList();
            var elements = _context.PersonaElements.ToList();
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Id == viewModel.Id);
            if (persona != null)
            {
                persona.Name = viewModel.Name;
                persona.Level = viewModel.Level;
                persona.Arcana = viewModel.Arcana;
                persona.Stats = viewModel.Stats;
                persona.Description = viewModel.Description;
                persona.ResistElements = elements.Where(e => viewModel.ResistElementsId.Contains(e.Id)).Select(e => e).ToList();
                persona.WeakElements = elements.Where(e => viewModel.WeakElementsId.Contains(e.Id)).Select(e => e).ToList();
                persona.Skills = skills.Where(s => viewModel.SelectSkillsId.Contains(s.Id)).Select(s => s).ToList();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Persona");
        }

        // GET: Persona/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetElements()
        {
            List<SelectListItem> elements = await _context.PersonaElements.AsNoTracking()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToListAsync();
            var elementTip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Element ---"
            };
            elements.Insert(0, elementTip);
            return new SelectList(elements, "Value", "Text");
        }

        private async Task<IEnumerable<SelectListItem>> GetSkills()
        {
            List<SelectListItem> skills = await _context.PersonaSkills.AsNoTracking()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SkillName
                }).ToListAsync();

            return new SelectList(skills, "Value", "Text");
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}

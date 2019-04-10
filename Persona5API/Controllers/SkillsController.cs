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
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            var personaContext = _context.PersonaSkills.Include(s => s.Cost).Include(s => s.Element);
            return View(await personaContext.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var persona = _context.Personas.Where(x => x.Skills == ).Select(x => x).ToList();
            var skills = await _context.PersonaSkills
                .Include(s => s.Cost)
                .Include(s => s.Element)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // GET: Skill/Create
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new SkillsFormViewModel
            {
                Elements = GetElements(),
                Cost = GetSkillCosts()
            };

            return View(viewModel);
        }

        // POST: Skill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(SkillsFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var elements = GetElements();
            var skillCosts = GetSkillCosts();
            SkillCost skillCost = _context.SkillCosts.FirstOrDefault(x => x.Id.ToString() == viewModel.SelectedCost);
            var costAmount = skillCost.Resource == "hp" ? skillCost.Amount + " %" + skillCost.Resource : (skillCost.Resource == "sp" ?
                        skillCost.Amount + skillCost.Resource : skillCost.Amount);
            var skills = new Skills
            {
                SkillName = viewModel.SkillName,
                Effect = viewModel.Effect,
                Cost = _context.SkillCosts.FirstOrDefault(c => c.Id.ToString() == viewModel.SelectedCost),
                CostAmount = skillCost.Amount + skillCost.Resource,
                Power = viewModel.Power,
                Element = _context.PersonaElements.FirstOrDefault(e => e.Name.ToString() == viewModel.SelectedElement),
                ElementName = viewModel.SelectedElement
            };
            _context.PersonaSkills.Add(skills);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: Skills/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var skillsCost = GetSkillCosts();
            var elements = GetElements();
            var skills = await _context.PersonaSkills
                .Include(s => s.Cost)
                .Include(e => e.Element)
                .FirstOrDefaultAsync(s => s.Id == id);           

            if (skills == null)
            {
                return NotFound();
            }

            var viewModel = new SkillsFormViewModel()
            {
                Id = skills.Id,
                SkillName = skills.SkillName,
                Effect = skills.Effect,
                Power = skills.Power,
                Cost = skillsCost,
                SelectedCost = skills.Cost.Id.ToString(),
                Elements = elements,
                SelectedElement = skills.Element.Id.ToString()
            };

            //ViewData["CostId"] = new SelectList(_context.SkillCosts, "Id", "Amount", skills.CostId);
            //ViewData["ElementId"] = new SelectList(_context.PersonaElements, "Name", "Name", skills.ElementId);
            return View(viewModel);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SkillName,Effect,Power,CostId,ElementId")] Skills skills)
        {
            if (id != skills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillsExists(skills.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostId"] = new SelectList(_context.SkillCosts, "Id", "Id", skills.CostId);
            ViewData["ElementId"] = new SelectList(_context.PersonaElements, "Id", "Id", skills.ElementId);
            return View(skills);
        }

        // GET: Skills/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skills = await _context.PersonaSkills
                .Include(s => s.Cost)
                .Include(s => s.Element)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skills = await _context.PersonaSkills.FindAsync(id);
            _context.PersonaSkills.Remove(skills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IEnumerable<SelectListItem> GetElements()
        {
            List<SelectListItem> elements = _context.PersonaElements.AsNoTracking()
                .Select(e => new SelectListItem
                {
                    Value = e.Name,
                    Text = e.Name
                }).ToList();
            var elementTip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Element ---"
            };
            elements.Insert(0, elementTip);
            return new SelectList(elements, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetSkillCosts()
        {

            List<SelectListItem> costs = _context.SkillCosts.AsNoTracking()
                .Select(c => new SelectListItem
                {
                    //Value = c.Resource == "hp" ? c.Amount + "% " + c.Resource.ToUpper() : (c.Resource == "sp" ?
                    //    c.Amount + " SP" : c.Amount),
                    //Text = c.Resource == "hp" ? c.Amount + "% " + c.Resource.ToUpper() : (c.Resource == "sp" ? 
                    //    c.Amount + " SP" : c.Amount)
                    Value = c.Id.ToString(),
                    Text = c.Amount + " " + c.Resource
                }).ToList();
            var costTip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Skill Cost ---"
            };
            costs.Insert(0, costTip);
            return new SelectList(costs, "Value", "Text");
        }

        private bool SkillsExists(int id)
        {
            return _context.PersonaSkills.Any(e => e.Id == id);
        }
    }
}

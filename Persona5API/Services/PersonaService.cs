using Microsoft.EntityFrameworkCore;
using Persona5API.Data;
using Persona5API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persona5API.Services
{
    public class PersonaService : IPersonaService
    {
        private ApplicationDbContext _context;

        public PersonaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Persona> AllIncluding(params Expression<Func<Persona, object>>[] includeProperties)
        {
            IQueryable<Persona> query = _context.Set<Persona>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
            //throw new NotImplementedException();
        }

        public Task<List<Persona>> GetAll()
        {
            return _context.Set<Persona>().ToListAsync();
            //throw new NotImplementedException();
        }

        public Persona GetSingle(System.Linq.Expressions.Expression<Func<Persona, bool>> predicate)
        {
            return _context.Set<Persona>().FirstOrDefault(predicate);
            //throw new NotImplementedException();
        }
    }
}

using Persona5API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persona5API.Services
{
    public interface IPersonaService
    {
        Task<List<Persona>> GetAll();
        IEnumerable<Persona> AllIncluding(params Expression<Func<Persona, object>>[] includeProperties);
        Persona GetSingle(Expression<Func<Persona, bool>> predicate);
    }
}

using Microsoft.EntityFrameworkCore;
using Persona5API.Data;
using Persona5API.Models;
using Persona5API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace web_api_test
{
    public class PersonaAppContext
    {
        private readonly List<Persona> _persona;

        
        //public PersonaAppContext()
        //{
        //    _persona = new List<Persona>()
        //    {
        //        new Persona()
        //        {
        //            Id = 4,
        //            Name = "Arsene",
        //            Level = 1,
        //            Arcana = "Fool",
        //            Stats =
        //            {
        //                Id = 1,
        //                Strength = 2,
        //                Magic = 2,
        //                Endurance = 2,
        //                Agility = 3,
        //                Luck = 1
        //            },
        //            Description = "A being based off the main character of Maurice Leblanc's novels, Arsene Lupin. He appears everywhere and is a master of disguise. He is known to help law-abiding citizens."
        //        }
        //    };
        //}

        //public IEnumerable<Persona> GetAll()
        //{
        //    return _persona;
        //}

        //public IEnumerable<Persona> AllIncluding(params Expression<Func<Persona, object>>[] includeProperties)
        //{
        //    IQueryable<Persona> query = _persona.AsQueryable();
        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return query.ToList();
        //}

        //public Persona GetSingle(Expression<Func<Persona, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

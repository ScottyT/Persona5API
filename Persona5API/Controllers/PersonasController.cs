using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persona5API.Models;
using Persona5API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persona5Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;

        public PersonasController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            if (_ctx.Personas.Count() == 0)
            {
                _ctx.Personas.Add(new Persona { Name = "Arsene", Arcana = "Fool", Level = 1 });
                _ctx.SaveChanges();
            }
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Persona>> Get()
        {
            return await _ctx.Personas.Include(x => x.Stats).ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

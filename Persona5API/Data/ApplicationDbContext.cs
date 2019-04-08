using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persona5API.Models;

namespace Persona5API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Skills> PersonaSkills { get; set; }
        public DbSet<SkillCost> SkillCosts { get; set; }
        public DbSet<Elements> PersonaElements { get; set; }      
    }
}

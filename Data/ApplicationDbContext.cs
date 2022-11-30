using CosultaNomina.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CosultaNomina.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<ConceptoNomina> ConceptoNomina { get; set; }
        public DbSet<RegSolicitudesIngresos> RegSolicitudesIngresos { get; set; }
    }
}
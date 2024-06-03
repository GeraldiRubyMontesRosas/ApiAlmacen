using Almacen.Entities;
using Microsoft.EntityFrameworkCore;

namespace Almacen
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
    }
}

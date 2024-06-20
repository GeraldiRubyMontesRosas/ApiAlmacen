using Almacen.Entities;
using Microsoft.EntityFrameworkCore;

namespace Almacen
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Define tus DbSets aquí
        public DbSet<Area> Areas { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Responsable> Responsables { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
    }
}

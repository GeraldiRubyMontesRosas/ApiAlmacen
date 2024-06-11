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
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Responsable> Responsables { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        
    }
}

using Almacen.Entities;
using System.ComponentModel.DataAnnotations;

namespace Almacen.DTOs
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string Nombre { get; set; }
        
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        [Required]
        public RolDTO Rol { get; set; }
        public ResponsableDTO? Responsable { get; set; }
    }
}

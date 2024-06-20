using Almacen.Entities;

namespace Almacen.DTOs
{
    public class TrasladoDTO
    {
        public int? Id { get; set; }
        public InmuebleDTO Inmueble { get; set; }
        public AreaDTO AreaOrigen { get; set; }
        public AreaDTO AreaDestino { get; set; }
        public UsuarioDTO? Usuario { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
    }
}

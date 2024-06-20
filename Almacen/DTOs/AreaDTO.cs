using Almacen.Entities;

namespace Almacen.DTOs
{
    public class AreaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
        public ResponsableDTO? Responsable { get; set; }

    }
}

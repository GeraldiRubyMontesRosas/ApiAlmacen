using Almacen.Entities;

namespace Almacen.DTOs
{
    public class InmuebleDTO
    {
        public int? Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; } 
        public Area Area { get; set; }  
        public string? Imagen { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? Qr { get; set; }
        public string? QrBase64 { get; set; }
        public string? PDF { get; set; }
        public string? PDFBase64 { get; set; }
        public float Costo { get; set; }
        public bool Estatus { get; set; }
    }
}

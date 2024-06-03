namespace Almacen.Entities
{
    public class Inmueble
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public Area Area { get; set; }
        public string? Imagen { get; set; }
        public string? Qr { get; set; }
        public bool Estatus { get; set; }

    }
}

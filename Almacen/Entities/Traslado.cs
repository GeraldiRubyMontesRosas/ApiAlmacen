namespace Almacen.Entities
{
    public class Traslado
    {
        public int Id { get; set; }
        public Inmueble Inmueble { get; set; }
        public Area AreaOrigen { get; set; }
        public Area AreaDestino { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
    }
}

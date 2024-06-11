namespace Almacen.Entities
{
    public class Responsable
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public string? UsuarioCreacionNombre { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string? UsuarioEdicionNombre { get; set; }
        public DateTime? FechaHoraEdicion { get; set; }
    
    }
}

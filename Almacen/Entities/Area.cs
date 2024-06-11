namespace Almacen.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
        public Responsable Responsable { get; set; }
    }
}

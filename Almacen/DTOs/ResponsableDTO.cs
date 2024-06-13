namespace Almacen.DTOs
{
    public class ResponsableDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string? NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string? StrFechaNacimiento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public int Edad => CalcularEdad(FechaNacimiento);
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;
            return edad;
        }
    }
}

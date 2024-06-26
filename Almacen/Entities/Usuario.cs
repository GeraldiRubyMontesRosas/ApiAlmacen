﻿using System.ComponentModel;

namespace Almacen.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        public Rol Rol { get; set; }
        public int? ResponsableId { get; set; }
        public Responsable? Responsable { get; set; }
    }
}

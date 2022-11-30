using System;
using System.ComponentModel.DataAnnotations;

namespace CosultaNomina.Models
{
    public class RegSolicitudesIngresos
    {
        [Key]
        public int ID_SOLICITUD { get; set; }
        public string? DOCUMENTO { get; set; }
        public string? APELLIDOS { get; set; }
        public string? NOMBRES { get; set; }
    }
}
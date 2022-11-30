using System;
using System.ComponentModel.DataAnnotations;

namespace CosultaNomina.Models
{
    public class ConceptoNomina
    {
        [Key]
        public int REGISTRO { get; set; }
        [Display(Name = "COD. CONCEPTO")]
        public int COD_CONCEPTO { get; set; }
        [Display(Name = "DESC CONCEPTO")]
        public string? DESC_CONCEPTO { get; set; }
        [Display(Name = "ID EMPLEADO")]
        public int ID_EMPLEADO { get; set; }
        public string? DOCUMENTO { get; set; }
        public string? APELLIDOS { get; set; }
        public string? NOMBRES { get; set; }
        [Display(Name = "VALOR NOMINA")]
        public int VAL_NOMINA { get; set; }
        public int CANTIDAD { get; set; }
    }
}   
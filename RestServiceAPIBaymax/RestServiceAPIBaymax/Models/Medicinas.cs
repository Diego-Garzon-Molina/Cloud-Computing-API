using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestServiceBaymax.Models
{
    public class Medicina
    {
        [Key]
        public int MedicinaId { get; set; }
        public string NombreMedicina { get; set; }
        public string PrincipiosActivos { get; set; }
        [Required]
        public int CodigoNacional { get; set; }
        public decimal Contenido { get; set; }
        public string UnidadContenido { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestServiceBaymax.Models
{
    public class MedicinasUsuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string NombresMedicinas { get; set; }
        public string PrincipiosActivos { get; set; }
        public int CodigosNacionales { get; set; }
        public int ContenidosIniciales { get; set; }
        public int ContenidosActuales { get; set; }
        public string UnidadContenido { get; set; }
    }
}
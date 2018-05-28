using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestServiceBaymax.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Genero { get; set; }
        public string tokensUsuariosQueNotifica { get; set; }
        public string tokensUsuariosQueRecibeNotificacion { get; set; }
    }
}
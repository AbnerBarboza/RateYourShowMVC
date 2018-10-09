using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Usuariolista
    {
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Lista")]
        [Key, Column(Order = 1)]
        public int ListaId { get; set; }

        public virtual Lista Lista { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
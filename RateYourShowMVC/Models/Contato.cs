using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Contato
    {
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Tipocontato")]
        [Key, Column(Order = 1)]
        public int TipocontatoId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Valor { get; set; }

        public virtual Tipocontato Tipocontato { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
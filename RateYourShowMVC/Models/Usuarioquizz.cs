using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Usuarioquizz
    {
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Perguntas")]
        [Key, Column(Order = 1)]
        public int PerguntaId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public Char Resposta { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Perguntas Perguntas { get; set; }
    }
}
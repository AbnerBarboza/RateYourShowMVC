using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Requisicao
    {
        [Key]
        public int RequisicaoId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime Data { get; set; }
        
        [Required]
        [MaxLength(300)]
        [MinLength(3)]
        public string Nomedaserie { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Descricao { get; set; }

        public Tipo Tipo { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class RecuperarSenha
    {
        [Required]
        [Key]
        public int SenhaId { get; set; }

        [Required]
        public int Codigo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime Vencimento { get; set; }

        [Required]
        public Boolean Valido { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
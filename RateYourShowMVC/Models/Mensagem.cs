using RateYourShowMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Mensagem
    {
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId1 { get; set; }

        [Required]
        [ForeignKey("Usuario2")]
        [Key, Column(Order = 1)]
        public int UsuarioId2 { get; set; }


        [Required]
        [MaxLength(1000)]
        public string Texto { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario2 { get; set; }
    }
}
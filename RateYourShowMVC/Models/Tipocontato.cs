using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Tipocontato
    {
        [Key]
        public int TipocontatoId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Contato { get; set; }
    }
}
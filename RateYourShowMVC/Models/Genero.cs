using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Genero
    {
        [Key]
        public int GeneroId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Descricao { set; get; }

    }
}
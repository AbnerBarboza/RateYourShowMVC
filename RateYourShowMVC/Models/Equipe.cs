using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Equipe
    {
        [Key]
        public int EquipeId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        public DateTime Datadenascimento { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Nacionalidade { get; set; }

        public virtual ICollection<Midia> Midia { get; set; }
        public virtual ICollection<SerieEquipe> SerieEquipe { get; set; }

    }
}
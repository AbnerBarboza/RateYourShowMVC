using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Series
    {
        [Key]
        public int SeriesId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Paisdeorigem { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string Descricao { get; set; }

        [Required]
        public Inativo Inativo { get; set; }

        [ForeignKey("Quizzes")]
        public int? QuizzesId { get; set; }

        public virtual Quizzes Quizzes { get; set; }
        public virtual ICollection<Midia> Midia { get; set; }
        public virtual ICollection<SerieEquipe> SerieEquipe { get; set; }
        public virtual ICollection<SerieGenero> SerieGenero { get; set; }
        public virtual ICollection<Usuarioserie> Usuarioserie { get; set; }

    }
}
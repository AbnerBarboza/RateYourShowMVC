using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Episodio
    {
        [Key]
        public int EpisodioId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [Required]
        public int Temporada { get; set; }

        [Required]
        [ForeignKey("Series")]
        public int SeriesId { get; set; }

        public virtual Series Series { get; set; }
    }
}
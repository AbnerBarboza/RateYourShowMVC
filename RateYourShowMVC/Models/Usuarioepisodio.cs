using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Usuarioepisodio
    {
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Episodio")]
        [Key, Column(Order = 1)]
        public int EpisodioId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public virtual Episodio Episodio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
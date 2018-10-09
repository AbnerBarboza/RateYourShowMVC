using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Publicacao
    {
        [Key]
        public int PublicacaoId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Descricao { get; set; }

        [Required]
        public int Episodio { get; set; }

        [Required]
        public int Temporada { get; set; }

        [Required]
        public Inativo Inativo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Series")]
        public int SeriesId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Series Series { get; set; }
        public virtual ICollection<Midia> Midia { get; set; }

    }
}
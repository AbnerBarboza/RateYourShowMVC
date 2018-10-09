using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{

    public class Midia
    {
        [Key]
        public int MidiaId { get; set; }

        [Required]
        [ForeignKey("TipoMidia")]
        public int TipoMidiaId { get; set; }

        [Required]
        public string Link { get; set; }

        [ForeignKey("Lista")]
        public int? ListaId { get; set; }

        [ForeignKey("Equipe")]
        public int? EquipeId { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }

        [ForeignKey("Series")]
        public int? SeriesId { get; set; }

        [ForeignKey("Publicacao")]
        public int? PublicacaoId { get; set; }

        public virtual TipoMidia TipoMidia { get; set; }
        public virtual Lista Lista { get; set; }
        public virtual Equipe Equipe { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Series Series { get; set; }
        public virtual Publicacao Publicacao { get; set; }

    }
}
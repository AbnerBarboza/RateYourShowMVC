using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Comentario
    {
        [Key]
        public int ComentarioId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Texto { get; set; }

        [Required]
        public DateTime Datadepublicacao { get; set; }

        [Required]
        [ForeignKey("Usuario2")]
        public int UsuarioId1 { get; set; }

        [ForeignKey("Series")]
        public int SeriesId { get; set; }

        [ForeignKey("Publicacao")]
        public int PublicacaoId { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Inativo Inativo { get; set; }

        public virtual Lista Lista { get; set; }
        public virtual Series Series { get; set; }
        public virtual Publicacao Publicacao { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario2 { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Denuncia
    {
        [Key]
        public int DenunciaId { get; set; }

        [MaxLength(255)]
        public String Descricao { get; set; }

        [Required]
        public DateTime Data{ get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId1 { get; set; }

        [Required]
        [ForeignKey("Usuario2")]
        public int UsuarioId2 { get; set; }

        [Required]
        [ForeignKey("Comentario")]
        public int ComentarioId { get; set; }

        [Required]
        [ForeignKey("Publicacao")]
        public int PublicacaoId { get; set; }

        [Required]
        [ForeignKey("Lista")]
        public int ListaId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario2 { get; set; }
        public virtual Comentario Comentario { get; set; }
        public virtual Publicacao Publicacao { get; set; }
        public virtual Lista Lista { get; set; }
    }
}
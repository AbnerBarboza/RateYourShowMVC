using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{

    public class Usuarioserie
    {
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Series")]
        [Key, Column(Order = 1)]
        public int SeriesId { get; set; }

        [Required]
        public Boolean Spoiler { get; set; }        
    
        [DefaultValue(0)]
        [Range(-1,1)]
        public int Avaliacao { get; set; }

        public DateTime Datedeinicio { get; set; }

        public Seguindo Seguindo { get; set; }

        public virtual Series Series { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
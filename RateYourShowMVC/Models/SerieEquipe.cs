using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class SerieEquipe
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        [Key, Column(Order = 3)]
        public string Personagem { get; set; }

        [Required]
        [ForeignKey("Equipe")]
        [Key, Column(Order = 0)]
        public int EquipeId{ get; set; }
        
        [Required]
        [ForeignKey("Series")]
        [Key, Column(Order = 1)]
        public int SeriesId { get; set; }
        
        [Required]
        [ForeignKey("EquipeTipo")]
        [Key, Column(Order = 2)]
        public int EquipeTipoId { get; set; }

        public virtual EquipeTipo EquipeTipo { get; set; }
        public virtual Equipe Equipe { get; set; }
        public virtual Series Series { get; set; }
    }
}
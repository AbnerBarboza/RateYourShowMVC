using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class SerieGenero
    {
        [Required]
        [ForeignKey("Series")]
        [Key, Column(Order = 0)]
        public int SerieID { get; set; }
        
        [Required]
        [ForeignKey("Genero")]
        [Key, Column(Order = 1)]
        public int GeneroID { get; set; }


        public virtual Genero Genero { get; set; }
        public virtual Series Series { get; set; }
    }
}
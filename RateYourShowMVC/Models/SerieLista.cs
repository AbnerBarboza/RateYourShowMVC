using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class SerieLista
    {
        
        [Required]
        [ForeignKey("Series")]
        [Key, Column(Order = 0)]
        public int SeriesID { get; set; }

        [Required]
        [ForeignKey("Lista")]
        [Key, Column(Order = 1)]
        public int ListaID { get; set; }

        [Display(Name = "Total de votos")]
        public int TotalVotos{ get; set; }
        public virtual Lista Lista { get; set; }
        public virtual Series Series { get; set; }

    }
}
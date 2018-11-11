using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Amizade
    {
        
        [Required]
        [ForeignKey("Usuario")]
        [Key, Column(Order = 0)]
        public int UsuarioId1 { get; set; }
        
        [Required]
        [ForeignKey("Usuario2")]
        [Key, Column(Order = 1)]
        public int UsuarioId2 { get; set; }

        [Required]
        [DefaultValue('P')]
        public Status Status { get; set; }
        
        [Required]
        public DateTime Datadeconvite {get;set;}

        public DateTime? Dataresposta { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario2 { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class EquipeTipo
    {
        [Key]
        public int EquipeTipoId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Descricao { get; set; }

    }
}
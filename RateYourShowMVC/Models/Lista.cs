using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{

    public class Lista
    {

        [Key]
        public int ListaId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [MinLength(3)]
        [MaxLength(200)]
        public string Descricao { get; set; }


        [Required]
        public TipoLista TipoLista { get; set; }

        [Required]
        public Inativo Inativo { get; set; }

        public virtual ICollection<Midia> Midia { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(250)]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(300)]
        [MinLength(6)]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatadeNascimento { get; set; }

        public Boolean SerieNotificacoes { get; set; }

        public Boolean EpiNotificacoes { get; set; }

        public Boolean Spoiler { get; set; }

        [Required]
        public Sexo Sexo { get; set; }

        [Required]
        public Inativo Inativo { get; set; }

        [Required]
        public Bloqueado Bloqueado { get; set; }

        [Required]
        public TipoUsuario TipoUsuario { get; set; }

        public virtual ICollection<Midia> Midia { get; set; }


    }

}
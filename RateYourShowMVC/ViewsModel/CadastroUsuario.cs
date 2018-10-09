using RateYourShowMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.ViewsModel
{
    public class CadastroUsuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "É necessário inserir um Nome!")]
        [MinLength(3, ErrorMessage = "O nome precisa conter 3 ou mais caracteres.")]
        [MaxLength(250, ErrorMessage = "O nome não pode conter mais de 250 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É necessário inserir um E-mail!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail Inválido!")]
        public string Email { get; set; }

        [Required]
        [MaxLength(300)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem!")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "É necessário inserir sua Data de Nascimento!")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DatadeNascimento { get; set; }

        [Required]
        public Sexo Sexo { get; set; }

        [Required]
        public Boolean TermosdeUso { get; set; }
    }
}
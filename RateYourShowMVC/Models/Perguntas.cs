using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Perguntas
    {
        [Key]
        public int PerguntasId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Pergunta { set; get; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Alternativa1 { set; get; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Alternativa2 { set; get; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Alternativa3 { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Alternativa4 { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Resposta { get; set; }

        [Required]
        [ForeignKey("Quizzes")]
        public int QuizzesId { get; set; }
        public virtual Quizzes Quizzes { get; set; }

    }
}
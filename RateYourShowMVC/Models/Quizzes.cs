using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public class Quizzes
    {
        [Key]
        public int QuizzesId { get; set; }

        [Required]
        public int Numerodeperguntas { get; set; }

    }
}
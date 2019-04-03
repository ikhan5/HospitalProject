using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Question
    {
        [Key, ScaffoldColumn(false)]
        public int QuestionID { get; set; }

        [Required, StringLength(255), Display(Name = "Question")]
        public string QuestionList { get; set; }

        [Required, StringLength(255), Display(Name = "Answer")]
        public string AnswerList { get; set; }
    }
}


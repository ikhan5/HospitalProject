using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models.newsletter
{
    public class Newsletter
    {
        [Key]
        public int news_ID { get; set; }

        [Required, StringLength(255), Display(Name = "News Name")]
        public string news_name { get; set; }

        [Required, StringLength(255), Display(Name = "Date")]
        public string date_created { get; set; }

        [Required, StringLength(255), Display(Name = "Content")]
        public string news_content { get; set; }

        public bool is_confirmed { get; set; }

        [ForeignKey("UserID")]
        public string UserID { get; set; }

    }
}

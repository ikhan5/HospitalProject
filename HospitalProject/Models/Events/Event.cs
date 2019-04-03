using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models.Events
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required, StringLength(255), Display(Name = "Event Name")]
        public string event_name { get; set; }

        [Required, StringLength(255), Display(Name = "Date")]
        public string event_date { get; set; }

        [Required, StringLength(255), Display(Name = "Description")]
        public string event_descp { get; set; }


        [ForeignKey("UserID")]
        public string UserID { get; set; }
    }
}

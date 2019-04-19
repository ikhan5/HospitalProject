using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Rating
    {
        [Key, ScaffoldColumn(false)]
        public int RatingID { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        //One doctors to many ratings
        public virtual Doctor Doctors { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Feedback")]
        public string Feedback { get; set; }


    }
}

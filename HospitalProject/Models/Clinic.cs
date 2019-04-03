using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Clinic
    {
        [Key]
        public int clinic_id { get; set; }

        [Required, StringLength(255), Display(Name = "Clinic name")]
        public string clinic_name { get; set; }

        [Required, StringLength(255), Display(Name = "Clinic description")]
        public string clinic_description { get; set; }

        [Required, StringLength(255), Display(Name = "Medical Services:")]
        public string clinic_services { get; set; }

        //[Required, StringLength(255), Display(Name = "Phone no:")]
        //public string clinic_phone { get; set; }

        [DataType(DataType.PhoneNumber), Display(Name = "Phone no:")]
        public string clinic_phone { get; set; }

        [Required, StringLength(255), Display(Name = "Location:")]
        public string clinic_location { get; set; }

        //This is a one to many relationship between clinic and medical services

        //[ForeignKey("medical_services_id")]
        //public string clinic_id { get; set; }

        //public virtual ApplicationUser user { get; set; }

    }
}


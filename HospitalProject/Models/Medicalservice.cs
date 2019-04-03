using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{

    public class Medicalservice
    {
        [Key]
        public int medical_services_id { get; set; }

        [Required, StringLength(255), Display(Name = "Medical Service name")]
        public string medical_services_name { get; set; }

        [Required, StringLength(255), Display(Name = "Medical Service description")]
        public string medical_services_description { get; set; }

        [Required, StringLength(255), Display(Name = "Type of Medical Services (Outbound/Inbound)")]
        public string medical_service_type { get; set; }

        /*[Required,Display(Name = "Type of Medical Services")]
        public TypeService medical_service_type { get; set; }*/

    }

   /* public enum TypeService
    {
        Outbound,
        Inbound
    }*/

    //This is a one to one relationship between medical services and clinic
    //[ForeignKey("medical_services_name")]
    //public string clinic_id { get; set; }

    //public virtual ApplicationUser user { get; set; }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Required(ErrorMessage = "Please select a medical service type"), Display(Name = "Type of Medical Services")]
        public string medical_service_type { get; set; }

        /*  [Required, StringLength(255), Display(Name = "Type of Medical Services (Outbound/Inbound)")]
          public string medical_service_type { get; set; }*/


    }

}



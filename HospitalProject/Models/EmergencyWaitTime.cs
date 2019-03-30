using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class EmergencyWaitTime
    {
        [Key]
        public int EmergencyWaitTimeID { get; set; }

        [Required, StringLength(255), Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Required, StringLength(255), Display(Name = "Wait Time")]
        public String WaitTime { get; set; }
    }
}

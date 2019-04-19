using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class VolunteerPost
    {
        [Key]
        public int VolunteerPostID { get; set; }

        [Required, StringLength(255), Display(Name = "Position")]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }

        [Required, StringLength(int.MaxValue), Display(Name = "Details")]
        public string Details { get; set; }

        [Required, StringLength(255), Display(Name = "Department")]
        public string Department { get; set; }

        [InverseProperty("VolunteerPosts")]
        public virtual List<VolunteerApplication> VolunteerApplications { get; set; }
    }
}


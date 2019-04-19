using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class VolunteerApplication
    {
        [Key]
        public int VolunteerAppID { get; set; }

        [Required, StringLength(255), Display(Name = "First Name")]
        public string AppFName { get; set; }

        [Required, StringLength(255), Display(Name = "Last Name")]
        public string AppLName { get; set; }

        [Required, StringLength(255), Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required, StringLength(255), Display(Name = "Email")]
        public string Email { get; set; }

        [Required, StringLength(3), Display(Name = "Age")]
        public int Age { get; set; }

        [Required, StringLength(int.MaxValue), Display(Name = "Descriptions")]
        public string Descriptions { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppDate { get; set; }

        [ForeignKey("VolunteerPostID")]
        public int VolunteerPostID { get; set; }

        public virtual VolunteerPost VolunteerPosts { get; set; }
    }
}


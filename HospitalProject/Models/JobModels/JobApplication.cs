using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class JobApplication
    {
        [Key]
        public int jobApplicationID { get; set; }

        [Required, StringLength(255), Display(Name = "Name")]
        public string applicantName { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Email")]
        public string applicantEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime applicationDate { get; set; }

        //Each Job Application has a Job Posting ID
        [ForeignKey("jobPostingID")]
        public int jobPostingID { get; set; }
        // Job Application belongs to a Job Posting
        public virtual JobPosting JobPosting { get; set; }

    }
}

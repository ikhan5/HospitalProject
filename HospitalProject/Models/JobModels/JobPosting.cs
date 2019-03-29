using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HospitalProject.Models
{
    public class JobPosting
    {
        [Key]
        public int jobPostingID { get; set; }

        [Required, StringLength(255), Display(Name = "Name of Cause")]
        public string jobTitle { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Job Qualifications")]
        public string jobQualifications { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Job Description")]
        public string jobDescription { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Required Skill")]
        public string jobSkills { get; set; }

        [DataType(DataType.Date)]
        public DateTime jobPostingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime jobExpiryDate { get; set; }

        //One Job Posting will have many Job Applications
        [InverseProperty("JobPosting")]
        public virtual List<JobApplication> JobApplication { get; set; }
    }
}

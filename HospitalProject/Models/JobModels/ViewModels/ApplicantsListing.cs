using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.JobModels.ViewModels
{
    public class ApplicantsListing
    {
        public ApplicantsListing()
        {

        }
        public virtual JobPosting jobPosting  { get; set; }
        public IEnumerable<JobApplication> jobApplications { get; set; }
    }
}

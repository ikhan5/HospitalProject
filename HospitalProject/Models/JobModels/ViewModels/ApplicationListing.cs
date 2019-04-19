using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.JobModels.ViewModels
{
    public class ApplicationListing
    {
        public ApplicationListing()
        {

        }

        public virtual JobApplication jobApplication { get; set; }
        public IEnumerable<JobPosting> jobPostings { get; set; }
    }
}

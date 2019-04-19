using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.VolunteerPostModels.ViewModels
{
    public class VolunteerPostList
    {
        public VolunteerPostList()
        {

        }

        public virtual VolunteerApplication VolunteerApplications { get; set; }
        public IEnumerable<VolunteerPost> VolunteerPosts { get; set; }
    }
}

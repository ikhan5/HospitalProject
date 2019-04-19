using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.VolunteerViews
{
    public class VoluneteerApplicationList
    {
        public VoluneteerApplicationList()
        {

        }

        public virtual VolunteerPost VoluneteerPosts { get; set; }
        public IEnumerable<VolunteerApplication> VolunteerApplications { get; set; }
    }
}

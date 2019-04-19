using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.ViewModels
{
    public class VolunteerPostList
    {
        public VolunteerPostList()
        {

        }

        public virtual VolunteerApplication volunteerapplications { get; set; }
        public IEnumerable<VolunteerPost> volunteerposts { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.ViewModels
{
    public class ListDoctors
    {
        public ListDoctors()
        {

        }

        public virtual Doctor doctors { get; set; }
        public IEnumerable<Rating> rating { get; set; }
    }
}

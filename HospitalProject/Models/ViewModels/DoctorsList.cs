using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.ViewModels
{
    public class DoctorsList
    {
        public DoctorsList()
        {
        }

        public virtual Rating rating { get; set; }
        public IEnumerable<Doctor> doctor { get; set; }
    }
}

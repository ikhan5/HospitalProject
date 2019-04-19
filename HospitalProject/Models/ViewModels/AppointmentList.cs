using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class AppointmentList
    {
        public AppointmentList()
        {
        }

        public virtual Appointment appointment { get; set; }
        public IEnumerable<Doctor> doctors { get; set; }
    }
}

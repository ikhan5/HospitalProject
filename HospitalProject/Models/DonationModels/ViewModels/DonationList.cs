using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.VolunteerViewsModels.ViewModels
{
    public class DonationList
    {
        public DonationList()
        {

        }

        public virtual DonationForm donationForm { get; set; }
        public IEnumerable<Donation> donations { get; set; }
    }
}

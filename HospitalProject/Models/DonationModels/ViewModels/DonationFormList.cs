using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.VolunteerViewsModels.ViewModels
{
    public class DonationFormList
    {

        public DonationFormList()
        {

        }   

        public virtual Donation donation { get; set; }
        public IEnumerable<DonationForm> donationForms { get; set; }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models.DonationModels
{
    public class DonationForm
    {
        [Key]
        public int donationFormID { get; set; }

        [Required, StringLength(255), Display(Name = "Name of Cause")]
        public string donationCause { get; set; }

        [Required, StringLength(255), Display(Name = "Organization Name")]
        public string charityName { get; set; }

        [Display(Name = "Goal($)")]
        public int donationGoal { get; set; }

        [StringLength(255), Display(Name = "Preset Amounts ($)")]
        public string presetAmounts { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Description")]
        public string formDescription { get; set; }

        //One Donation Form will have many Donations
        [InverseProperty("DonationForm")]
        public List<Donation> Donations { get; set; }
    }
}

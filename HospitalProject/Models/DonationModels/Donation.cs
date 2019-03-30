using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public enum PaymentMethod
    {
        Credit,
        PayPal
    }

    public enum Occurence
    {
        Once,
        Month
    }

    public class Donation
    {
        [Key]
        public int donationID { get; set; }

        [StringLength(255), Display(Name = "Name")]
        public string donorName { get; set; }

        [Required(ErrorMessage = "Please Enter your Email"), StringLength(255), Display(Name = "Email")]
        public string donorEmail { get; set; }

        [Display(Name = "Donation Occurence")]
        public Occurence isRecurring { get; set; }

        [Required(ErrorMessage = "Please select a Payment Method"), Display(Name = "Payment Method")]
        public PaymentMethod paymentMethod { get; set; }

        [Display(Name = "Donation Amount")]
        public int paymentAmount { get; set; }

        //Each Donation has a Donation Form ID
        [ForeignKey("donationFormID")]
        public int donationFormID { get; set; }
        // Donation belongs to a Donation Form
        public virtual DonationForm DonationForm { get; set; }

    }
}

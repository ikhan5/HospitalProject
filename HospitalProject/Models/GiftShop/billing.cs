using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models.GiftShop
{
    public class Billing
    {
        [Key]
        public int BillingID { get; set; }

        public int Total { get; set; }

        [Required, StringLength(255), Display(Name = "Payment Methods")]
        public string Paymentmethods { get; set; }

        [ForeignKey("CartID")]
        public string CartID { get; set; }

        public virtual Cart cart { get; set; }

    }
}

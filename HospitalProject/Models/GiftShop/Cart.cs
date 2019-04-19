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
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [Required, StringLength(255), Display(Name = "Total")]
        public float Total { get; set; }

        [Required, StringLength(255), Display(Name = "Payment Methods")]
        public string Paymentmethods { get; set; }

        //One Cart will have many items
        [InverseProperty("Cart")]
        public List<Item> Items { get; set; }



        
    }
}

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
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required, StringLength(255), Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required, StringLength(255), Display(Name = "Item Description")]
        public string ItemDescp { get; set; }

        [Required, Display(Name = "Item Price")]
        public int price { get; set; }

        [Required, Display(Name = "Quantity")]
        public int quantity { get; set; }

        //Each cart has a cart ID
        [ForeignKey("CartID")]
        public int CartID { get; set; }
        // item belongs to a cart
        public virtual Cart cart { get; set; }

    }
}

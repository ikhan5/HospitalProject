using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class ParkingService
    {
        [Key]
        public int ParkingServiceID { get; set; }

        public int ParkingNumber { get; set; }

        [Required, StringLength(255), Display(Name = "Rate")]
        public string Rate { get; set; }

        public bool Status { get; set; }

        //This is a one to one relationship between parkingservice and users
        //In this case the UserID is a string in AspNetUsers
        [ForeignKey("UserID")]
        public string ParkingServiceUserID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}

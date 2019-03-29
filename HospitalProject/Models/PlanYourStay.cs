using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class PlanYourStay
    {
        [Key]
        public int PlanYourStayID { get; set; }

        [Required, StringLength(255), Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Required, StringLength(255), Display(Name = "Rate Per Day")]
        public string RatePerDay { get; set; }

        [Required, StringLength(255), Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        //This is a one to one relationship between planyourstay and users
        //In this case the UserID is a string in AspNetUsers
        [ForeignKey("UserID")]
        public string PlanYourStayUserID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //Configure one to one relationship between user and planyourstay
        [ForeignKey("PlanYourStayID")]
        public int? PlanYourStayID { get; set; }
        
        //Configure one to one relationship between user and planyourservice
        [ForeignKey("ParkingServiceID")]
        public int? ParkingServiceID { get; set; }

        //Configure one to one relationship between user and author
        [ForeignKey("AdminID")]
        public int? AdminID { get; set; }

        public virtual Admin admin { get; set; }
    }
}

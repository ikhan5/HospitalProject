using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public enum Status
        {
            User,
            Admin
        }
        [Required(ErrorMessage = "Please choose an option"), StringLength(255), Display(Name = "User Status: ")]
        public bool UserStatus { get; set; }

        [Required(ErrorMessage = "Please choose an option"), Display(Name = "Type: ")]
        public Status UserType { get; set; }

        //This is a one to one relationship between authors and users
        //In this case the UserID is a string in AspNetUsers
        [ForeignKey("UserID")]
        public string UserID { get; set; }

        public virtual ApplicationUser admin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Doctor
    {
        //doctor ID 
        [Key, ScaffoldColumn(false)]
        public int DoctorID { get; set; }


        //doctor name
        [Required, StringLength(255), Display(Name = "Name")]
        public string DoctorName { get; set; }

        //doctor department
        [Required, StringLength(255), Display(Name = "Department")]
        public string DoctorDepartment { get; set; }

        //doctor description
        [Required, StringLength(255), Display(Name = "Description")]
        public string DoctorDescription { get; set; }

        //One doctor has one department 
       // [InverseProperty("Doctors")]
       // public virtual Department Departments { get; set; }

        //One doctor has many ratings
         [InverseProperty("Doctors")]
         public virtual List<Rating> Ratings { get; set; }
        


    }
}



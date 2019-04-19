using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Appointment
    {
        [Key]
        public int client_id { get; set; }

        [Required, StringLength(255), Display(Name = "First Name")]
        public string client_fname { get; set; }

        [Required, StringLength(255), Display(Name = "Last Name")]
        public string client_lname { get; set; }

        [Required, StringLength(255), Display(Name = "Phone no:")]
        public string client_phone { get; set; }

        [Required, StringLength(255), Display(Name = "Email Address:")]
        public string client_emailadd { get; set; }

        /*[Required, StringLength(255), Display(Name = "Regular Dr:")]
        public string client_doctor_id { get; set; }*/

          //Each Donation has a Donation Form ID
        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        // Donation belongs to a Donation Form
        public virtual Doctor Doctors { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Date and Time:")]
        public DateTime date_time { get; set; }

        [Required, StringLength(255), Display(Name = "Appointment Details:")]
        public string appointment_details { get; set; }

        //[ForeignKey("UserID")]
        //public string UserID { get; set; }

        //public virtual ApplicationUser user { get; set; }
    }
}


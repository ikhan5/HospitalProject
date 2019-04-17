/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Department
    {
        [Key, ScaffoldColumn(false)]
        public int DepartmentID { get; set; }

        [Required, StringLength(255), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        //This is how we can represent a one (dept) to many (doctors) relation
        //notice how if we were using a relational database this column
        //would be included as a foreign of authorid in the pages table.
        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctors { get; set; }
    }
}
*/

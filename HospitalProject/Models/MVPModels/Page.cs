using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Page
    {
        [Key]
        public int pageID { get; set; }

        [Required, StringLength(255), Display(Name = "Page Author")]
        public string pageAuthor { get; set; }

        [Required, StringLength(255), Display(Name = "Page Title")]
        public string pageTitle { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Page Content")]
        public string pageContent { get; set; }

        [StringLength(int.MaxValue), Display(Name = "Required Skill")]
        public string jobSkills { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime lastModified { get; set; }

        [Display(Name = "Page Order")]
        public int pageOrder { get; set; }

        //Each Page has a Navigation ID
        [ForeignKey("navigationID")]
        public int navigationID { get; set; }
        // Donation belongs to a Donation Form
        public virtual Navigation Navigation { get; set; }
    }
}

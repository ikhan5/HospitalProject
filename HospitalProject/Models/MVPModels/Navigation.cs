using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models.MVPModels
{
    public class Navigation
    {
        [Key]
        public int navigationID { get; set; }

        [Required, StringLength(255), Display(Name = "Header Name")]
        public string navigationName { get; set; }

        [StringLength(255), Display(Name = "URL")]
        public string navigationURL { get; set; }

        [Required, Display(Name = "Position")]
        public int navigationPosition { get; set; }

        //One Navigation will have many Pages
        [InverseProperty("Navigation")]
        public List<Page> Pages { get; set; }
    }
}

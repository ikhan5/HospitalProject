using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class ReferAPatient
    {
        [Key]
        public int ReferAPatientID { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReferalDate { get; set; }

        [Required, StringLength(255), Display(Name = "Patient Name")]
        public string PatName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required, StringLength(255), Display(Name = "OHIP")]
        public string OHIP { get; set; }

        [Required, StringLength(int.MaxValue), Display(Name = "Patient Address")]
        public string PatAddress { get; set; }

        [Required, StringLength(255), Display(Name = "Patient Phone")]
        public string PatPhone { get; set; }

        [Required, StringLength(255), Display(Name = "Patient email")]
        public string PatEmail { get; set; }

        [Required, StringLength(255), Display(Name = "Program Required")]
        public string ProgReq { get; set; }

        [Required, StringLength(int.MaxValue), Display(Name = "Current Diagnosis")]
        public string CurrPrimDiag { get; set; }

        [Required, StringLength(int.MaxValue), Display(Name = "Medical History")]
        public string MedHist { get; set; }

        [Required, StringLength(int.MaxValue), Display(Name = "Service Required")]
        public string ServiceReq { get; set; }

        [Required, StringLength(255), Display(Name = "Referring Facility")]
        public string ReferFac { get; set; }

        [Required, StringLength(255), Display(Name = "Physician Name")]
        public string ReferPhysName { get; set; }

        [Required, StringLength(255), Display(Name = "Physician Phone")]
        public string ReferPhysPhone { get; set; }

        [Required, StringLength(255), Display(Name = "Physician Email")]
        public string ReferPhysEmail { get; set; }
    }
}

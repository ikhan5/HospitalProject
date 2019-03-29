using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using HospitalProject.Models;
using HospitalProject.Data;

namespace HospitalProject.Data
{
    public class HospitalCMSContext : IdentityDbContext<    ApplicationUser>
    {
        public HospitalCMSContext(DbContextOptions<HospitalCMSContext> options)
        : base(options)
        {

        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Navigation> Navigations { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<DonationForm> DonationForms { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}

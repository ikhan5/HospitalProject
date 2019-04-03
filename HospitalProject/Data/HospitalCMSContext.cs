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
using HospitalProject.Models.GiftShop;
using HospitalProject.Models.newsletter;
using HospitalProject.Models.Events;
using HospitalProject.Models.JobModels;
using HospitalProject.Models.DonationModels;
using HospitalProject.Models.MVPModels;


namespace HospitalProject.Data
{
    public class HospitalCMSContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalCMSContext(DbContextOptions<HospitalCMSContext> options)
        : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<EmergencyWaitTime> EmergencyWaitTimes { get; set; }
        public DbSet<PlanYourStay> PlanYourStays { get; set; }
        public DbSet<ParkingService> ParkingServices { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Navigation> Navigations { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<DonationForm> DonationForms { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Event> Events { get; set; }
     
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Medicalservice> Medicalservices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<VolunteerPost> VolunteerPosts { get; set; }
        public DbSet<VolunteerApplication> VolunteerApplications { get; set; }
        public DbSet<ReferAPatient> ReferAPatients { get; set; }
        

        //I need the code which actually makes this into a table.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //also need to specify that these models make tables
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<EmergencyWaitTime>().ToTable("EmergencyWaitTimes");
            modelBuilder.Entity<PlanYourStay>().ToTable("PlanYourStays");
            modelBuilder.Entity<ParkingService>().ToTable("ParkingServices");

            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Rating>().ToTable("Ratings");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<DonationForm>().ToTable("DonationForms");

        }
    }
}

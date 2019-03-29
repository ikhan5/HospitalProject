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
    public class HospitalCMSContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalCMSContext(DbContextOptions<HospitalCMSContext> options)
        : base(options)
        {

        }
  
        public DbSet<Admin> Admin { get; set; }
        public DbSet<EmergencyWaitTime> EmergencyWaitTimes { get; set; }
        public DbSet<PlanYourStay> PlanYourStays { get; set; }
        public DbSet<ParkingService> ParkingServices { get; set; }


        //I need the code which actually makes this into a table.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //also need to specify that these models make tables
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<EmergencyWaitTime>().ToTable("EmergencyWaitTimes");
            modelBuilder.Entity<PlanYourStay>().ToTable("PlanYourStays");
            modelBuilder.Entity<ParkingService>().ToTable("ParkingServices");
        }
    }
}

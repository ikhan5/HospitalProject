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


        public DbSet<Admin> admin { get; set; }
    }
}

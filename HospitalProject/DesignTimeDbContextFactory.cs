using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalProject.Models;
using HospitalProject.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HospitalCMSContext>
    {
        public HospitalCMSContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<HospitalCMSContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new HospitalCMSContext(builder.Options);
        }
    }
}

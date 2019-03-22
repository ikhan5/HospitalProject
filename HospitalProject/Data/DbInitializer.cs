using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(HospitalCMSContext context)
        {
            context.Database.EnsureCreated();

            return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.MVPModels.ViewModels
{
    public class NavigationListing
    {
        public NavigationListing()
        {

        }

        public virtual Page page { get; set; }
        public IEnumerable<Navigation> navigations { get; set; }
    }
}

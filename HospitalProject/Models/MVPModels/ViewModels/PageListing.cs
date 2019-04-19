using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.MVPModels.ViewModels
{
    public class PageListing
    {

        public PageListing()
        {

        }

        public virtual Navigation navigation { get; set; }
        public IEnumerable<Page> pages { get; set; }
    }
}

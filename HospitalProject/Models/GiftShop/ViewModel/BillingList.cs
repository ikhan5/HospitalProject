using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.GiftShop.ViewModel
{
    public class BillingList
    {
        public BillingList()
        {

        }

        public virtual Cart cart { get; set; }
        public IEnumerable<Billing> billings { get; set; }

    }

    
}

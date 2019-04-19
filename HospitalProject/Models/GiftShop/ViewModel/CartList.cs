using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.GiftShop.ViewModel
{
    public class CartList
    {
        public CartList()
        {

        }

        public virtual Item item { get; set; }
        public IEnumerable<Cart> carts { get; set; }

    }
}

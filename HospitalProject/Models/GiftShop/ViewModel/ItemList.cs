using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalProject.Models.GiftShop.ViewModel
{
    public class ItemList
    {
        public ItemList()
        {

        }

        public virtual Cart carts { get; set; }
        public IEnumerable<Item> item { get; set; }
    }
}

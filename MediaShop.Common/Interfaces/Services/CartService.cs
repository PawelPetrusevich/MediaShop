using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Models
{
    public class CartService : Interfaces.Services.ICartService
    {
        private readonly Interfaces.Repositories.ICartRespository<ContentCart> store;

        public CartService(Interfaces.Repositories.ICartRespository<ContentCart> store)
        {
            store = store;
        }

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCart> GetItemsInCart(int id)
        {
            var itemsInCart = this.store.Find(x => x.Id == id);
            return itemsInCart;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models;

namespace MediaShop.Common.Interfaces.Services
{
    public class CartService : ICartService<Models.ContentCart>
    {
        private readonly Interfaces.Repositories.ICartRespository<Models.ContentCart> store;

        public CartService(Interfaces.Repositories.ICartRespository<Models.ContentCart> store)
        {
            store = store;
        }

        public Cart GetCart(int id)
        {
            throw new NotImplementedException();
        }

        public uint GetCoutItems(int id)
        {
            throw new NotImplementedException();
        }

        public uint GetCoutItems(IEnumerable<ContentCart> cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<Models.ContentCart> GetItemsInCart(int id)
        {
            var itemsInCart = this.store.Find(x => x.Id == id);
            return itemsInCart;
        }

        public float GetPrice(int id)
        {
            throw new NotImplementedException();
        }

        public float GetPrice(IEnumerable<ContentCart> cart)
        {
            throw new NotImplementedException();
        }
    }
}

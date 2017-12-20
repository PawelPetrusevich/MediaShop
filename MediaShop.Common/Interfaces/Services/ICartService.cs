namespace MediaShop.Common.Interfaces.Services
{
    using Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the service when working with the ShoppingCart
    /// </summary>
    public interface ICartService<TModel>
        where TModel : Models.Entity
    {
        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        IEnumerable<TModel> GetItemsInCart(int id);
    }
}

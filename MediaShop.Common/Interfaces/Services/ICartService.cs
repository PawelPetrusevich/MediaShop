namespace MediaShop.Common.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the service when working with the Cart
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface ICartService<TModel>
        where TModel : Models.Entity
    {
        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        IEnumerable<TModel> GetItemsInCart(int id);

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Cart</returns>
        Models.Cart GetCart(int id);

        /// <summary>
        /// Get sum price items for User
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Sum price</returns>
        float GetPrice(int id);

        /// <summary>
        /// Get sum price items typeof ContentCart
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Sum price</returns>
        float GetPrice(IEnumerable<TModel> cart);

        /// <summary>
        /// Get count items for User
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Count Items in cart</returns>
        uint GetCoutItems(int id);

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Count Items</returns>
        uint GetCoutItems(IEnumerable<TModel> cart);
    }
}

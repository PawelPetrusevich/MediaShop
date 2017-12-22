namespace MediaShop.Common.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the service when working with the Cart
    /// </summary>
    /// <typeparam name="PModel">type model1</typeparam>
    /// <typeparam name="TModel">type model2</typeparam>
    public interface ICartService<PModel, TModel>
        where PModel : Entity
        where TModel : ContentCart
    {
        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        IEnumerable<TModel> GetItemsInCart(int id);

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="content">identificator media content </param>
        /// <returns>this save item</returns>
        TModel AddNewContentInCart(PModel content);

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="id">content identificator</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        bool FindContentInCart(int id);

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Cart</returns>
        Cart GetCart(int id);

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
        uint GetCountItems(int id);

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Count Items</returns>
        uint GetCountItems(IEnumerable<TModel> cart);
    }
}

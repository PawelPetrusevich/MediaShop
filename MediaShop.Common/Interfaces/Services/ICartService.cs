namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;
    using MediaShop.Common.Enums;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the service when working with the Cart
    /// </summary>
    /// <typeparam name="TModel">type model2</typeparam>
    public interface ICartService<TModel>
        where TModel : ContentCart
    {
        /// <summary>
        /// Find items in a cart by user Id and return a items collection
        /// </summary>
        /// <param name="userId">users id</param>
        /// <param name="contentState">contents state</param>
        /// <returns> shopping cart for a user </returns>
        IEnumerable<ContentCart> GetInCart(ulong userId, CartEnums.StateCartContent contentState);

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <returns>this save item</returns>
        TModel AddInCart(ulong contentId, ulong userId);

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="contentId">content identificator</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        bool FindInCart(ulong contentId);

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        Cart GetCart(ulong userId);

        /// <summary>
        /// Get sum price items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Sum price</returns>
        decimal GetPrice(ulong userId);

        /// <summary>
        /// Get sum price items typeof ContentCart
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Sum price</returns>
        decimal GetPrice(IEnumerable<TModel> cart);

        /// <summary>
        /// Get count items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Count Items in cart</returns>
        uint GetCountItems(ulong userId);

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Count Items</returns>
        uint GetCountItems(IEnumerable<TModel> cart);

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="itemsId">collection users id</param>
        /// <returns>collection of remote objects</returns>
        ICollection<ContentCart> DeleteOfCart(ICollection<ulong> itemsId);

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <param name="contentState">contents state</param>
        /// <returns>update objects state</returns>
        ContentCart SetState(
            ulong contentId, ulong userId, CartEnums.StateCartContent contentState);
    }
}

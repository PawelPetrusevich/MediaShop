namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the service when working with the Cart
    /// </summary>
    /// <typeparam name="PModel">type model1</typeparam>
    /// <typeparam name="TModel">type model2</typeparam>
    public interface ICartService<PModel, TModel>
        where PModel : ContentClassForUnitTest
        where TModel : ContentCart
    {
        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        IEnumerable<TModel> GetItemsInCart(ulong id);

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="content">identificator media content </param>
        /// <param name="userId">user identifier</param>
        /// <returns>this save item</returns>
        TModel AddNewContentInCart(PModel content, ulong userId);

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="id">content identificator</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        bool FindContentInCart(ulong id);

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Cart</returns>
        Cart GetCart(ulong id);

        /// <summary>
        /// Get sum price items for User
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Sum price</returns>
        decimal GetPrice(ulong id);

        /// <summary>
        /// Get sum price items typeof ContentCart
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Sum price</returns>
        decimal GetPrice(IEnumerable<TModel> cart);

        /// <summary>
        /// Get count items for User
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Count Items in cart</returns>
        uint GetCountItems(ulong id);

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Count Items</returns>
        uint GetCountItems(IEnumerable<TModel> cart);

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>collection of remote objects</returns>
        ICollection<ContentCartDto> DeleteContentFromCart(ulong userId);

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>amount of object that deleted</returns>
        int DeleteAllContentFromCart(ulong userId);

        /// <summary>
        /// Method for check object CounterId as Bought
        /// </summary>
        /// <param name="contentId">contents id</param>
        /// <param name="userId">user id</param>
        /// <returns>changes object</returns>
        TModel SetContentAsBought(ulong contentId, ulong userId);

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="contentId">id content</param>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>object that checked for
        /// the control his condition</returns>
        bool CheckedContent(ulong contentId, ulong userId);

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="contentId">id content</param>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>object that checked for
        /// the control his condition</returns>
        bool UnCheckedContent(ulong contentId, ulong userId);
    }
}

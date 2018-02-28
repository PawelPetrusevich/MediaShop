namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaShop.Common.Enums;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the service when working with the Cart
    /// </summary>
    /// <typeparam name="TModel">type model2</typeparam>
    public interface ICartService<TModel>
        where TModel : ContentCartDto
    {
        /// <summary>
        /// Find items in a cart by user Id and return a items collection
        /// </summary>
        /// <param name="userId">users id</param>
        /// <returns> shopping cart for a user </returns>
        IEnumerable<TModel> GetContent(long userId);

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// without state InPaid and InBought
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns> shopping cart for a user </returns>
        Task<IEnumerable<TModel>> GetContentAsync(long userId);

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        TModel AddInCart(long contentId, long userId);

        /// <summary>
        /// Async add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        Task<TModel> AddInCartAsync(long contentId, long userId);

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="contentId">content identificator</param>
        /// <param name="userId">users identifier</param>
        /// <param name="contentState">contents state</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        bool ExistInCart(long contentId, long userId, CartEnums.StateCartContent contentState);

        /// <summary>
        /// Async checking the existence of content in cart
        /// </summary>
        /// <param name="contentId">content identificator</param>
        /// <param name="userId">users identifier</param>
        /// <param name="contentState">contents state</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        Task<bool> ExistInCartAsync(long contentId, long userId, CartEnums.StateCartContent contentState);

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        Cart GetCart(long userId);

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        Task<Cart> GetCartAsync(long userId);

        /// <summary>
        /// Get sum price items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Sum price</returns>
        decimal GetPrice(long userId);

        /// <summary>
        /// Get sum price items typeof ContentCartDto
        /// </summary>
        /// <param name="cart">Collection ContentCartDto</param>
        /// <returns>Sum price</returns>
        decimal GetPrice(IEnumerable<TModel> cart);

        /// <summary>
        /// Get count items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Count Items in cart</returns>
        uint GetCountItems(long userId);

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCartDto</param>
        /// <returns>Count Items</returns>
        uint GetCountItems(IEnumerable<TModel> cart);

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="model">model ContentCartDto for delete</param>
        /// <returns>return deleting  model element</returns>
        TModel DeleteContent(TModel model);

        /// <summary>
        /// Method for deleting selected ContentCart
        /// </summary>
        /// <param name="model">model ContentCartDto for delete</param>
        /// <returns>return deleting  model element</returns>
        Task<TModel> DeleteContentAsync(TModel model);

        /// <summary>
        /// Method for deleting selected ContentCart
        /// </summary>
        /// <param name="id">ContentCart Id for delete</param>
        /// <returns>return count</returns>
        Task<int> DeleteContentAsync(long id);

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="itemsId">collection users id</param>
        /// <returns>collection of remote objects</returns>
        ICollection<ContentCartDto> DeleteOfCart(ICollection<long> itemsId);

        /// <summary>
        /// Method for deleting Content from cart
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart after clearing</returns>
        Cart DeleteOfCart(long userId);

        /// <summary>
        /// Method for deleting Content from cart
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart after clearing</returns>
        Task<Cart> DeleteOfCartAsync(long userId);

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users identifier</param>
        /// <param name="contentState">contents state</param>
        /// <returns>update objects state</returns>
        ContentCartDto SetState(
            long contentId, long userId, CartEnums.StateCartContent contentState);

        /// <summary>
        /// Async method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users identifier</param>
        /// <param name="contentState">contents state</param>
        /// <returns>update objects state</returns>
        Task<ContentCartDto> SetStateAsync(
            long contentId, long userId, CartEnums.StateCartContent contentState);
    }
}

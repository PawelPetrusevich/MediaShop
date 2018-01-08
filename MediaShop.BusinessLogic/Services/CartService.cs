namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AutoMapper;
    using MediaShop.Common.Enums;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<ContentCart>
    {
        private readonly ICartRepository<ContentCart> repositoryContentCart;

        private readonly IProductRepository<Product> repositoryProduct;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentCartRepository">instance repository CartRepository</param>
        /// <param name="productRepository">instance repository ProductRepository</param>
        public CartService(ICartRepository<ContentCart> contentCartRepository, IProductRepository<Product> productRepository)
        {
            this.repositoryContentCart = contentCartRepository;
            this.repositoryProduct = productRepository;
        }

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        public ContentCart AddInCart(long contentId, long userId)
        {
            //// Get object ProductDto by id
            var product = this.repositoryProduct.Get(contentId);

            if (product == null)
            {
                throw new CartExceptions($"Product with id = {contentId} is absent in database");
            }

            // Mapping object Entity to object ContentCart
            var contentCart = Mapper.Map<ContentCart>(product);

            // Initialize CreatorId
            contentCart.CreatorId = userId;

            // Save object ContentCartDto in repository
            var addContentCart = this.repositoryContentCart.Add(contentCart);

            // If the object is not added to the database
            // return null
            if (addContentCart == null)
            {
                throw new CartExceptions($"Content cart with id = {contentId} isn`t add in cart");
            }

            // Mapping object ContentCartDto to ContentCart
            return addContentCart;
        }

        /// <summary>
        /// Method for deleting selected contentCart
        /// </summary>
        /// <param name="collectionId">collection users id</param>
        /// <returns>collection of remote objects</returns>
        public ICollection<ContentCart> DeleteOfCart(ICollection<long> collectionId)
        {
            if (collectionId == null)
            {
                throw new NullReferenceException();
            }

            var collectionContentCart = new Collection<ContentCart>();
            foreach (long contentCart in collectionId)
            {
                var deleteContentCart = this.repositoryContentCart.Delete(contentCart);
                if (deleteContentCart == null)
                {
                    // To do rollback
                    if (collectionContentCart.Count != 0)
                    {
                        foreach (ContentCart content in collectionContentCart)
                        {
                            var addContentCartRollback = this.repositoryContentCart.Add(content);
                        }
                    }

                    throw new CartExceptions(
                        $"Operation delete unsuccesfully. Content cart with id = {contentCart} isn`t delete in cart");
                }
                else
                {
                    collectionContentCart.Add(deleteContentCart);
                }
            }

            return collectionContentCart;
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="contentid">content id</param>
        /// <returns>true - content exist in cart
        /// false - content doesn`t exist in cart</returns>
        public bool FindInCart(long contentid) => this.repositoryContentCart
            .Get(contentid) != null;

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// without state InPaid and InBought
        /// </summary>
        /// <param name="id">user Id</param>
        /// <param name="contentState">contents state</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCart> GetInCart(long id, CartEnums.StateCartContent contentState)
        {
            var contentsInCart = this.repositoryContentCart.Find(
                x => x.CreatorId == id && x.StateContent == contentState);
            List<ContentCart> collectionContentCarts = new List<ContentCart>();
            foreach (ContentCart contentCart in contentsInCart)
            {
                collectionContentCarts.Add(contentCart);
            }

            return collectionContentCarts;
        }

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <param name="contentState">contents state</param>
        /// <returns>object with update state</returns>
        public ContentCart SetState(long contentId, long userId, CartEnums.StateCartContent contentState)
        {
            // Get object by id
            var contentCartForUpdate = this.repositoryContentCart.Get(contentId);

            if (contentCartForUpdate == null)
            {
                throw new CartExceptions($"Product with id = {contentId} is absent in database");
            }

            // change state object
            contentCartForUpdate.StateContent = contentState;

            // change ModifierId and ModifiedDate
            contentCartForUpdate.ModifiedDate = DateTime.Now;
            contentCartForUpdate.ModifierId = userId;

            // Update change
            var contentCartAfterUpdate = this.repositoryContentCart.Update(contentCartForUpdate);

            if (contentCartAfterUpdate.StateContent != contentState)
            {
                throw new CartExceptions($"State content with id = {contentCartAfterUpdate.Id} isn`t update");
            }

            // Return object ContentCart
            return contentCartAfterUpdate;
        }

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        public Cart GetCart(long userId)
        {
            var itemsInCart = this.GetInCart(userId, CartEnums.StateCartContent.InCart);
            var model = new Cart()
            {
                ContentCartCollection = itemsInCart,
                CountItemsInCollection = this.GetCountItems(itemsInCart),
                PriceAllItemsCollection = this.GetPrice(itemsInCart)
            };
            return model;
        }

        /// <summary>
        /// Get count items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Count Items in cart</returns>
        public uint GetCountItems(long userId)
        {
            var cart = this.GetInCart(userId, CartEnums.StateCartContent.InCart);
            return (uint)cart.Count();
        }

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Count Items</returns>
        public uint GetCountItems(IEnumerable<ContentCart> cart)
        {
            return (uint)cart.Count();
        }

        /// <summary>
        /// Get sum price items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Sum price</returns>
        public decimal GetPrice(long userId)
        {
            var cart = this.GetInCart(userId, CartEnums.StateCartContent.InCart);
            return cart.Sum<ContentCart>(x => x.PriceItem);
        }

        /// <summary>
        /// Get sum price items typeof ContentCart
        /// </summary>
        /// <param name="cart">Collection ContentCart</param>
        /// <returns>Sum price</returns>
        public decimal GetPrice(IEnumerable<ContentCart> cart)
        {
            return cart.Sum(x => x.PriceItem);
        }
    }
}

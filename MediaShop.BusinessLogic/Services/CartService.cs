﻿namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AutoMapper;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Enums;
    using MediaShop.Common.Exceptions.CartExseptions;
    using MediaShop.Common.Exceptions.PaymentExceptions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<ContentCartDto>
    {
        private readonly ICartRepository repositoryContentCart;

        private readonly IProductRepository repositoryProduct;

        private readonly IPaymentService servicePayment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentCartRepository">instance repository CartRepository</param>
        /// <param name="productRepository">instance repository ProductRepository</param>
        public CartService(ICartRepository contentCartRepository, IProductRepository productRepository, IPaymentService paymentService)
        {
            this.repositoryContentCart = contentCartRepository;
            this.repositoryProduct = productRepository;
            this.servicePayment = paymentService;
        }

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <returns>this save item</returns>
        public ContentCartDto AddInCart(long contentId)
        {
            // Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // Check exist Content in Cart
            if (this.ExistInCart(contentId))
            {
                throw new ExistContentInCartExceptions(Resources.ExistContentInCart);
            }

            var contentCart = new ContentCart() { Id = 1, CreatorId = 1, ProductId = contentId }; // Need initializing CreatorId !!!

            // Save object ContentCart in repository
            var addContentCart = this.repositoryContentCart.Add(contentCart);

            // If the object is not added to the database
            // return null
            if (addContentCart == null)
            {
                throw new AddContentInCartExceptions(Resources.ResourceManager.GetString("AddContentInCart"));
            }

            // Output mapping object ContentCart to object ContentCartDto
            return Mapper.Map<ContentCartDto>(addContentCart);
        }

        /// <summary>
        /// Method for deleting selected ContentCart
        /// </summary>
        /// <param name="model">model ContentCartDto for delete</param>
        /// <returns>return deleting  model element</returns>
        public ContentCartDto DeleteContent(ContentCartDto model)
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }

            if (model.StateContent != CartEnums.StateCartContent.InCart)
            {
                throw new DeleteContentInCartExseptions(Resources.StateContentError);
            }

            // Final mapping object ContentCartDto to object ContentCart
            var contentCart = Mapper.Map<ContentCart>(model);

            var deleteContentCart = this.repositoryContentCart.Delete(contentCart);
            if (deleteContentCart == null)
            {
                throw new DeleteContentInCartExseptions(Resources.DeleteContentFromCart);
            }

            // Output mapping object ContentCart to object ContentCartDto
            var deleteContentCartDto = Mapper.Map<ContentCartDto>(deleteContentCart);

            return deleteContentCartDto;
        }

        /// <summary>
        /// Method for deleting selected ContentCartDto
        /// </summary>
        /// <param name="collectionId">collection users id</param>
        /// <returns>collection of remote objects</returns>
        public ICollection<ContentCartDto> DeleteOfCart(ICollection<long> collectionId)
        {
            if (collectionId == null)
            {
                throw new NullReferenceException();
            }

            var collectionContentCart = new Collection<ContentCart>();
            foreach (long contentCartId in collectionId)
            {
                var deleteContentCart = this.repositoryContentCart.Delete(contentCartId);
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

                    throw new DeleteContentInCartExseptions(Resources.DeleteContentFromCart);
                }
                else
                {
                    collectionContentCart.Add(deleteContentCart);
                }
            }

            // Final collection object CollectionCartDto
            var collectionContentCartDto = new Collection<ContentCartDto>();

            // Output mapping ContentCart to ContentCartDto
            foreach (ContentCart contentCart in collectionContentCart)
            {
                var contentCartDto = Mapper.Map<ContentCartDto>(contentCart);
                collectionContentCartDto.Add(contentCartDto);
            }

            return collectionContentCartDto;
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <returns>true - content exist in cart
        /// false - content doesn`t exist in cart</returns>
        public bool ExistInCart(long contentId) => this.repositoryContentCart
            .Find(item => item.Product.Id == contentId) != null;

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// without state InPaid and InBought
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCartDto> GetContent(long id)
        {
            var contentInCart = this.repositoryContentCart.GetAll(id);
            return Mapper.Map<IEnumerable<ContentCartDto>>(contentInCart.Where(x => x.StateContent == CartEnums.StateCartContent.InCart));
        }

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="contentState">contents state</param>
        /// <returns>object with update state</returns>
        public ContentCartDto SetState(long contentId, CartEnums.StateCartContent contentState)
        {
            // Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // Get object by id
            var contentCartForUpdate = this.repositoryContentCart.Get(contentId);

            if (contentCartForUpdate == null)
            {
                throw new ExistContentInCartExceptions(Resources.NotExistContentInCart);
            }

            // Change state object
            contentCartForUpdate.StateContent = contentState;

            // Update change
            var contentCartAfterUpdate = this.repositoryContentCart.Update(contentCartForUpdate);

            // Check update property StateContent
            if (contentCartAfterUpdate.StateContent != contentState)
            {
                throw new UpdateContentInCartExseptions(Resources.UpdateContentInCart);
            }

            // Output mapping object ContentCart to object ContentCartDto
            return Mapper.Map<ContentCartDto>(contentCartAfterUpdate);
        }

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        public Cart GetCart(long userId)
        {
            var itemsInCart = this.GetContent(userId);
            var model = new Cart()
            {
                ContentCartDtoCollection = itemsInCart,
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
            var cart = this.GetContent(userId);
            return (uint)cart.Count();
        }

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCartDto</param>
        /// <returns>Count Items</returns>
        public uint GetCountItems(IEnumerable<ContentCartDto> cart)
        {
            return cart == null ? 0 : (uint)cart.Count();
        }

        /// <summary>
        /// Get sum price items for User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Sum price</returns>
        public decimal GetPrice(long userId)
        {
            var cart = this.GetContent(userId);
            return cart == null ? 0 : cart.Sum<ContentCartDto>(x => x.PriceItem);
        }

        /// <summary>
        /// Get sum price items typeof ContentCartDto
        /// </summary>
        /// <param name="cart">Collection ContentCartDto</param>
        /// <returns>Sum price</returns>
        public decimal GetPrice(IEnumerable<ContentCartDto> cart)
        {
            return cart == null ? 0 : cart.Sum(x => x.PriceItem);
        }

        /// <summary>
        /// Method for buy content
        /// </summary>
        /// <param name="contentId">content`s identificator</param>
        /// <returns>renew cart after buy content</returns>
        public Cart BuyContent(long contentId)
        {
            // 1. Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // 2. Get object Product by id
            var product = this.repositoryProduct.Get(contentId);

            if (product == null)
            {
                throw new NotExistProductInDataBaseExceptions(Resources.ExistProductInDataBase);
            }

            // 3. Change state content in cart on InBought
            var content = this.SetState(contentId, CartEnums.StateCartContent.InBought);

            // 4. Payment for the content by the buyer
            // Notification seller send in PaymentService
            var transactBuyContent = this.servicePayment.PaymentBayer(contentId);
            if (transactBuyContent == null)
            {
                // 4.1. Change state content in cart on InCart
                var contentRollBack = this.SetState(contentId, CartEnums.StateCartContent.InCart);
                throw new InvalidTransactionException(Resources.InvalidTransactionBuyContent);
            }

            // 5. Change state content in cart on InPaid
            var contentPaid = this.SetState(contentId, CartEnums.StateCartContent.InPaid);

            // 6. Open content for download

            // 7. Return new Cart without buy content
            var cartWithoutBuyContent = this.GetCart(1); // Need initialize accountId
            return cartWithoutBuyContent;
        }
    }
}

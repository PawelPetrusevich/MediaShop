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
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<ContentCartDto>
    {
        private readonly ICartRepository<ContentCartDto> repositoryContentCartDto;

        private readonly IProductRepository<Product> repositoryProduct;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentCartDtoRepository">instance repository CartRepository</param>
        /// <param name="productRepository">instance repository ProductRepository</param>
        public CartService(ICartRepository<ContentCartDto> contentCartDtoRepository, IProductRepository<Product> productRepository)
        {
            this.repositoryContentCartDto = contentCartDtoRepository;
            this.repositoryProduct = productRepository;
        }

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        public ContentCartDto AddInCart(long contentId, string categoryName)
        {
            // Verify string categoryName
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrWhiteSpace(categoryName))
            {
                throw new FormatException(Resources.NullOrEmptyValueString);
            }

            //// Get object ProductDto by id
            var product = this.repositoryProduct.Get(contentId);

            if (product == null)
            {
                throw new ExistContentInCartExceptions(Resources.ExistContentInCart);
            }

            // Mapping object Entity to object ContentCartDto
            var contentCartDto = Mapper.Map<ContentCartDto>(product);

            // Initialize CreatorId
            contentCartDto.CreatorId = 1; // Need initializing userId

            // Save object ContentCartDtoDto in repository
            var addContentCartDto = this.repositoryContentCartDto.Add(contentCartDto);

            // If the object is not added to the database
            // return null
            if (addContentCartDto == null)
            {
                throw new AddContentInCartExceptions(Resources.AddContentInCart);
            }

            // Mapping object ContentCartDtoDto to ContentCartDto
            return addContentCartDto;
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

            var collectionContentCartDto = new Collection<ContentCartDto>();
            foreach (long contentCartDto in collectionId)
            {
                var deleteContentCartDto = this.repositoryContentCartDto.Delete(contentCartDto);
                if (deleteContentCartDto == null)
                {
                    // To do rollback
                    if (collectionContentCartDto.Count != 0)
                    {
                        foreach (ContentCartDto content in collectionContentCartDto)
                        {
                            var addContentCartDtoRollback = this.repositoryContentCartDto.Add(content);
                        }
                    }

                    throw new DeleteContentInCartExseptions(Resources.DeleteContentFromCart);
                }
                else
                {
                    collectionContentCartDto.Add(deleteContentCartDto);
                }
            }

            return collectionContentCartDto;
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="contentid">content id</param>
        /// <returns>true - content exist in cart
        /// false - content doesn`t exist in cart</returns>
        public bool FindInCart(long contentid) => this.repositoryContentCartDto
            .Get(contentid) != null;

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// without state InPaid and InBought
        /// </summary>
        /// <param name="id">user Id</param>
        /// <param name="contentState">contents state</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCartDto> GetInCart(long id, CartEnums.StateCartContent contentState)
        {
            var contentsInCart = this.repositoryContentCartDto.Find(
                x => x.CreatorId == id && x.StateContent == contentState);
            List<ContentCartDto> collectionContentCartDtos = new List<ContentCartDto>();
            foreach (ContentCartDto contentCartDto in contentsInCart)
            {
                collectionContentCartDtos.Add(contentCartDto);
            }

            return collectionContentCartDtos;
        }

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <param name="contentState">contents state</param>
        /// <returns>object with update state</returns>
        public ContentCartDto SetState(long contentId, CartEnums.StateCartContent contentState)
        {
            // Get object by id
            var contentCartDtoForUpdate = this.repositoryContentCartDto.Get(contentId);

            if (contentCartDtoForUpdate == null)
            {
                throw new ExistContentInCartExceptions(Resources.ExistContentInCart);
            }

            // change state object
            contentCartDtoForUpdate.StateContent = contentState;

            // change ModifierId and ModifiedDate
            contentCartDtoForUpdate.ModifierId = 1; // Need initializing modifierId

            // Update change
            var contentCartDtoAfterUpdate = this.repositoryContentCartDto.Update(contentCartDtoForUpdate);

            if (contentCartDtoAfterUpdate.StateContent != contentState)
            {
                throw new UpdateContentInCartExseptions(Resources.UpdateContentInCart);
            }

            // Return object ContentCartDto
            return contentCartDtoAfterUpdate;
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
            var cart = this.GetInCart(userId, CartEnums.StateCartContent.InCart);
            return (uint)cart.Count();
        }

        /// <summary>
        /// Get count items
        /// </summary>
        /// <param name="cart">Collection ContentCartDto</param>
        /// <returns>Count Items</returns>
        public uint GetCountItems(IEnumerable<ContentCartDto> cart)
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
            return cart.Sum<ContentCartDto>(x => x.PriceItem);
        }

        /// <summary>
        /// Get sum price items typeof ContentCartDto
        /// </summary>
        /// <param name="cart">Collection ContentCartDto</param>
        /// <returns>Sum price</returns>
        public decimal GetPrice(IEnumerable<ContentCartDto> cart)
        {
            return cart.Sum(x => x.PriceItem);
        }
    }
}

namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AutoMapper;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Enums;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;
    using MediaShop.DataAccess.Repositories;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<ContentCart>
    {
        private readonly ICartRepository<ContentCartDto> repositoryCart;

        private readonly IProductRepository<ProductDto> repositoryProduct;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentRepo">instance repository CartRepository</param>
        public CartService(ICartRepository<ContentCartDto> contentRepo)
        {
            this.repositoryCart = contentRepo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentRepo">instance repository CartRepository</param>
        /// <param name="productRepo">instance repository ProductRepository</param>
        public CartService(ICartRepository<ContentCartDto> contentRepo, IProductRepository<ProductDto> productRepo)
        {
            this.repositoryCart = contentRepo;
            this.repositoryProduct = productRepo;
        }

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        public ContentCart Add(ulong contentId, ulong userId)
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

            // Mapping object ContentCart to ContentCartDto
            var contentCartDto = Mapper.Map<ContentCartDto>(contentCart);

            // Save object ContentCartDto in repository
            var contentAddDto = this.repositoryCart.Add(contentCartDto);

            // If the object is not added to the database
            // return null
            if (contentAddDto == null)
            {
                throw new CartExceptions($"Content cart with id = {contentId} isn`t add in cart");
            }

            // Mapping object ContentCartDto to ContentCart
            return Mapper.Map<ContentCart>(contentAddDto);
        }

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="itemsId">collection users id</param>
        /// <returns>collection of remote objects</returns>
        public ICollection<ContentCartDto> Delete(ICollection<ulong> itemsId)
        {
            if (itemsId == null)
            {
                throw new NullReferenceException();
            }

            var listRezalt = new Collection<ContentCartDto>();
            foreach (ulong item in itemsId)
            {
                var obj = this.repositoryCart.Delete(item);
                if (obj == null)
                {
                    throw new CartExceptions($"Content cart with id = {item} isn`t delete in cart");
                }

                listRezalt.Add(obj);
            }

            return listRezalt;
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="id">content id</param>
        /// <returns>true - content exist in cart
        /// false - content doesn`t exist in cart</returns>
        public bool Find(ulong id) => this.repositoryCart
            .Get(id) != null;

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCart> GetItems(ulong id)
        {
            var itemsInCart = this.repositoryCart.Find(x => x.CreatorId == id && x.StateContent != CartEnums.StateCartContent.InPaid);
            IList<ContentCart> itemsDto = new List<ContentCart>();
            foreach (ContentCartDto itemDto in itemsInCart)
            {
                itemsDto.Add(Mapper.Map<ContentCart>(itemDto));
            }

            return itemsDto;
        }

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <returns>update objects state</returns>
        public CartEnums.StateCartContent SetBought(ulong contentId, ulong userId)
        {
            // Get object by id
            var objectForUpdate = this.repositoryCart.Get(contentId);

            if (objectForUpdate == null)
            {
                throw new CartExceptions($"Product with id = {contentId} is absent in database");
            }

            // change state object
            objectForUpdate.StateContent = CartEnums.StateCartContent.InBought;

            // change ModifierId and ModifiedDate
            objectForUpdate.ModifiedDate = DateTime.Now;
            objectForUpdate.ModifierId = userId;

            // Update change
            var objectDtoUpdate = this.repositoryCart.Update(objectForUpdate);

            if (objectDtoUpdate.StateContent != CartEnums.StateCartContent.InBought)
            {
                throw new CartExceptions($"State content with id = {objectDtoUpdate.Id} isn`t update");
            }

            // Return object ContentCart with mapping object ContentCartDto to ContentCart
            return objectDtoUpdate.StateContent;
        }

        /// <summary>
        /// Method for check object as UnBought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <returns>update objects state</returns>
        public CartEnums.StateCartContent SetUnBought(ulong contentId, ulong userId)
        {
            // Get object by id
            var objectForUpdate = this.repositoryCart.Get(contentId);

            if (objectForUpdate == null)
            {
                throw new CartExceptions($"Product with id = {contentId} is absent in database");
            }

            // change state object
            objectForUpdate.StateContent = CartEnums.StateCartContent.InCart;

            // change ModifierId and ModifiedDate
            objectForUpdate.ModifiedDate = DateTime.Now;
            objectForUpdate.ModifierId = userId;

            // Update change
            var objectDtoUpdate = this.repositoryCart.Update(objectForUpdate);

            if (objectDtoUpdate.StateContent != CartEnums.StateCartContent.InCart)
            {
                throw new CartExceptions($"State content with id = {objectDtoUpdate.Id} isn`t update");
            }

            // Return object ContentCart with mapping object ContentCartDto to ContentCart
            return objectDtoUpdate.StateContent;
        }

        /// <summary>
        /// Method for check object as Paid
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <returns>update objects state</returns>
        public CartEnums.StateCartContent SetPaid(ulong contentId, ulong userId)
        {
            // Get object by id
            var objectForUpdate = this.repositoryCart.Get(contentId);

            if (objectForUpdate == null)
            {
                throw new CartExceptions($"Product with id = {contentId} is absent in database");
            }

            // change state object
            objectForUpdate.StateContent = CartEnums.StateCartContent.InPaid;

            // change ModifierId and ModifiedDate
            objectForUpdate.ModifiedDate = DateTime.Now;
            objectForUpdate.ModifierId = userId;

            // Update change
            var objectDtoUpdate = this.repositoryCart.Update(objectForUpdate);

            if (objectDtoUpdate.StateContent != CartEnums.StateCartContent.InPaid)
            {
                throw new CartExceptions($"State content with id = {objectDtoUpdate.Id} isn`t update");
            }

            // Return object ContentCart with mapping object ContentCartDto to ContentCart
            return objectDtoUpdate.StateContent;
        }

        /// <summary>
        /// Method for check object as UnPaid
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users id</param>
        /// <returns>update objects state</returns>
        public CartEnums.StateCartContent SetUnPaid(ulong contentId, ulong userId)
        {
            // Get object by id
            var objectForUpdate = this.repositoryCart.Get(contentId);

            if (objectForUpdate == null)
            {
                throw new CartExceptions($"Product with id = {contentId} is absent in database");
            }

            // change state object
            objectForUpdate.StateContent = CartEnums.StateCartContent.InBought;

            // change ModifierId and ModifiedDate
            objectForUpdate.ModifiedDate = DateTime.Now;
            objectForUpdate.ModifierId = userId;

            // Update change
            var objectDtoUpdate = this.repositoryCart.Update(objectForUpdate);

            if (objectDtoUpdate.StateContent != CartEnums.StateCartContent.InBought)
            {
                throw new CartExceptions($"State content with id = {objectDtoUpdate.Id} isn`t update");
            }

            // Return object ContentCart with mapping object ContentCartDto to ContentCart
            return objectDtoUpdate.StateContent;
        }

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        public Cart GetCart(ulong userId)
        {
            var itemsInCart = this.GetItems(userId);
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
        /// <param name="id">user Id</param>
        /// <returns>Count Items in cart</returns>
        public uint GetCountItems(ulong id)
        {
            var cart = this.GetItems(id);
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
        /// <param name="id">user Id</param>
        /// <returns>Sum price</returns>
        public decimal GetPrice(ulong id)
        {
            var cart = this.GetItems(id);
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

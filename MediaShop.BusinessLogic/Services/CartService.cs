namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Enums;
    using MediaShop.Common.Exceptions.CartExceptions;
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

        private readonly INotificationService serviceNotification;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentCartRepository">instance repository CartRepository</param>
        /// <param name="productRepository">instance repository ProductRepository</param>
        /// <param name="notificationService">instance service NotificationService</param>
        public CartService(ICartRepository contentCartRepository, IProductRepository productRepository, INotificationService notificationService)
        {
            this.repositoryContentCart = contentCartRepository;
            this.repositoryProduct = productRepository;
            this.serviceNotification = notificationService;
        }

        /// <summary>
        /// Add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        public ContentCartDto AddInCart(long contentId, long userId)
        {
            // Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // Check exist Product in repository
            if (this.repositoryProduct.Get(contentId) == null)
            {
                throw new ExistContentInCartExceptions(Resources.ExistProductInDataBase);
            }

            // Check exist Content in Cart with state = InBougth for current user
            if (this.ExistInCart(contentId, userId, CartEnums.StateCartContent.InPaid))
            {
                throw new ExistContentInCartExceptions(Resources.ContentAlreadyBougth);
            }

            // Check exist Content in Cart with state = InCart for current user
            if (this.ExistInCart(contentId, userId, CartEnums.StateCartContent.InCart))
            {
                throw new ExistContentInCartExceptions(Resources.ExistContentInCart);
            }

            var contentCart = new ContentCart() { CreatorId = userId, ProductId = contentId };

            // Save object ContentCart in repository
            var addContentCart = this.repositoryContentCart.Add(contentCart);

            // If the object is not added to the database
            // return null
            if (addContentCart == null)
            {
                throw new AddContentInCartExceptions(Resources.ResourceManager.GetString("AddContentInCart"));
            }

            // Get information about Product by Id
            var product = this.repositoryProduct.Get(contentId);

            // Create object AddToCartNotifyDto
            var objectNotify = new AddToCartNotifyDto() { ReceiverId = userId, ProductName = product.ProductName };

            var notification = this.serviceNotification.AddToCartNotify(objectNotify);

            // Create ContentCartDto
            var contentCartDto = Mapper.Map<ContentCartDto>(product);
            contentCartDto.Id = addContentCart.Id;
            contentCartDto.CreatorId = userId;

            // Output mapping object ContentCart to object ContentCartDto
            return contentCartDto;
        }

        /// <summary>
        /// Async add new item in cart with return save item for update view
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <param name="userId">users identifier</param>
        /// <returns>this save item</returns>
        public async Task<ContentCartDto> AddInCartAsync(long contentId, long userId)
        {
            // Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // Check exist Product in repository
            if (await this.repositoryProduct.GetAsync(contentId).ConfigureAwait(false) == null)
            {
                throw new ExistContentInCartExceptions(Resources.ExistProductInDataBase);
            }

            // Check exist Content in Cart with state = InBougth for current user
            if (await this.ExistInCartAsync(contentId, userId, CartEnums.StateCartContent.InPaid).ConfigureAwait(false))
            {
                throw new ExistContentInCartExceptions(Resources.ContentAlreadyBougth);
            }

            // Check exist Content in Cart with state = InCart for current user
            if (await this.ExistInCartAsync(contentId, userId, CartEnums.StateCartContent.InCart).ConfigureAwait(false))
            {
                throw new ExistContentInCartExceptions(Resources.ExistContentInCart);
            }

            var contentCart = new ContentCart() { CreatorId = userId, ProductId = contentId };

            // Save object ContentCart in repository
            var addContentCart = await this.repositoryContentCart.AddAsync(contentCart).ConfigureAwait(false);

            // If the object is not added to the database
            // return null
            if (addContentCart == null)
            {
                throw new AddContentInCartExceptions(Resources.ResourceManager.GetString("AddContentInCart"));
            }

            // Get information about Product by Id
            var product = await this.repositoryProduct.GetAsync(contentId).ConfigureAwait(false);

            // Create object AddToCartNotifyDto
            var objectNotify = new AddToCartNotifyDto() { ReceiverId = userId, ProductName = product.ProductName };

            var notification = await this.serviceNotification.AddToCartNotifyAsync(objectNotify);

            // Create ContentCartDto
            var contentCartDto = Mapper.Map<ContentCartDto>(product);
            contentCartDto.Id = addContentCart.Id;
            contentCartDto.CreatorId = userId;

            // Output mapping object ContentCart to object ContentCartDto
            return contentCartDto;
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
                throw new ArgumentNullException(Resources.NullOrEmptyValue, nameof(model));
            }

            if (model.StateContent != CartEnums.StateCartContent.InCart)
            {
                throw new DeleteContentInCartExceptions(Resources.StateContentError);
            }

            // Final mapping object ContentCartDto to object ContentCart
            var contentCart = Mapper.Map<ContentCart>(model);

            var deleteContentCart = this.repositoryContentCart.Delete(contentCart);
            if (deleteContentCart == null)
            {
                throw new DeleteContentInCartExceptions(Resources.DeleteContentFromCart);
            }

            // Output mapping object ContentCart to object ContentCartDto
            var deleteContentCartDto = Mapper.Map<ContentCartDto>(deleteContentCart);

            return deleteContentCartDto;
        }

        /// <summary>
        /// Method for deleting selected ContentCart
        /// </summary>
        /// <param name="model">model ContentCartDto for delete</param>
        /// <returns>return deleting  model element</returns>
        public async Task<ContentCartDto> DeleteContentAsync(ContentCartDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue, nameof(model));
            }

            if (model.StateContent != CartEnums.StateCartContent.InCart)
            {
                throw new DeleteContentInCartExceptions(Resources.StateContentError);
            }

            // Final mapping object ContentCartDto to object ContentCart
            var contentCart = Mapper.Map<ContentCart>(model);

            var deleteContentCart = await this.repositoryContentCart.DeleteAsync(contentCart).ConfigureAwait(false);
            if (deleteContentCart == 0)
            {
                throw new DeleteContentInCartExceptions(Resources.DeleteContentFromCart);
            }

            return model;
        }

        /// <summary>
        /// Method for deleting selected ContentCart
        /// </summary>
        /// <param name="id">ContentCart Id for delete</param>
        /// <returns>return count</returns>
        public async Task<int> DeleteContentAsync(long id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException(Resources.InvalidIdValue);
            }

            var result = await this.repositoryContentCart.DeleteAsync(id).ConfigureAwait(false);
            if (result == 0)
            {
                throw new DeleteContentInCartExceptions(Resources.DeleteContentFromCart);
            }

            return result;
        }

        /// <summary>
        /// Method for deleting selected ContentCartDto
        /// </summary>
        /// <param name="collectionId">collection content id</param>
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

                    throw new DeleteContentInCartExceptions(Resources.DeleteContentFromCart);
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
        /// Method for deleting Content from cart
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart after clearing</returns>
        public Cart DeleteOfCart(long userId)
        {
            var contentCollection = this.GetContent(userId);
            foreach (var content in contentCollection)
            {
                var deleteContentCart = this.DeleteContent(content);
            }

            return this.GetCart(userId);
        }

        /// <summary>
        /// Method for deleting Content from cart
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart after clearing</returns>
        public async Task<Cart> DeleteOfCartAsync(long userId)
        {
            var contentCollection = await this.GetContentAsync(userId).ConfigureAwait(false);
            foreach (var content in contentCollection)
            {
                var deleteContentCart = await this.DeleteContentAsync(content).ConfigureAwait(false);
            }

            return await this.GetCartAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <param name="userId">user identifier</param>
        /// <param name="contentState">content state</param>
        /// <returns>true - content exist in cart
        /// false - content doesn`t exist in cart</returns>
        public bool ExistInCart(long contentId, long userId, CartEnums.StateCartContent contentState)
        {
            var resultFind = this.repositoryContentCart
            .Find(item => item.ProductId == contentId &
                item.CreatorId == userId & item.StateContent == contentState);

            return resultFind.Count() != 0;
        }

        /// <summary>
        /// Async checking the existence of content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <param name="userId">user identifier</param>
        /// <param name="contentState">content state</param>
        /// <returns>true - content exist in cart
        /// false - content doesn`t exist in cart</returns>
        public async Task<bool> ExistInCartAsync(long contentId, long userId, CartEnums.StateCartContent contentState)
        {
            var resultFindAsync = await this.repositoryContentCart
                .FindAsync(item => item.ProductId == contentId &
                    item.CreatorId == userId & item.StateContent == contentState)
                .ConfigureAwait(false);

            return resultFindAsync.Count() != 0;
        }

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// without state InPaid and InBought
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCartDto> GetContent(long id)
        {
            var contentInCart = this.repositoryContentCart.GetById(id);
            return Mapper.Map<IEnumerable<ContentCartDto>>(contentInCart);
        }

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// without state InPaid and InBought
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public async Task<IEnumerable<ContentCartDto>> GetContentAsync(long id)
        {
            var contentInCart = await this.repositoryContentCart.GetByIdAsync(id).ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ContentCartDto>>(contentInCart);
        }

        /// <summary>
        /// Method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users identifier</param>
        /// <param name="contentState">contents state</param>
        /// <returns>update objects state</returns>
        public ContentCartDto SetState(long contentId, long userId, CartEnums.StateCartContent contentState)
        {
            // Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // Get object by contentId
            var contentCartForUpdateList = this.repositoryContentCart
                .Find(item => item.ProductId == contentId & item.CreatorId == userId & item.StateContent == CartEnums.StateCartContent.InCart)
                .ToList();

            if (contentCartForUpdateList.Count() == 0)
            {
                throw new ExistContentInCartExceptions(Resources.NotExistContentInCart);
            }

            contentCartForUpdateList[0].StateContent = contentState;

            // Update change
            var contentCartAfterUpdate = this.repositoryContentCart.Update(contentCartForUpdateList[0]);

            // Check update property StateContent
            if (contentCartAfterUpdate.StateContent != contentState)
            {
                throw new UpdateContentInCartExseptions(Resources.UpdateContentInCart);
            }

            // Output mapping object ContentCart to object ContentCartDto
            return Mapper.Map<ContentCartDto>(contentCartAfterUpdate);
        }

        /// <summary>
        /// Async method for check object as Bought
        /// </summary>
        /// <param name="contentId">contents object</param>
        /// <param name="userId">users identifier</param>
        /// <param name="contentState">contents state</param>
        /// <returns>object with update state</returns>
        public async Task<ContentCartDto> SetStateAsync(long contentId, long userId, CartEnums.StateCartContent contentState)
        {
            // Verify long contentId
            if (contentId <= 0)
            {
                throw new ArgumentException(Resources.InvalidContentId);
            }

            // Get object by contentId
            var contentCartForUpdateList = await this.repositoryContentCart
                .FindAsync(item => item.ProductId == contentId & item.CreatorId == userId & item.StateContent == CartEnums.StateCartContent.InCart)
                .ConfigureAwait(false);

            if (contentCartForUpdateList.Count() == 0)
            {
                throw new ExistContentInCartExceptions(Resources.NotExistContentInCart);
            }

            // Change state content
            var contentObjForUpdate = contentCartForUpdateList.AsQueryable().ElementAt(0);

            contentObjForUpdate.StateContent = contentState;

            // Update change
            var contentCartAfterUpdate = await this.repositoryContentCart.UpdateAsync(contentObjForUpdate).ConfigureAwait(false);

            // Check update property StateContent
            if (contentCartAfterUpdate.StateContent != contentState)
            {
                throw new UpdateContentInCartExseptions(Resources.UpdateContentInCart);
            }

            var contentCart = Mapper.Map<ContentCartDto>(contentCartAfterUpdate);

            // Output mapping object ContentCart to object ContentCartDto
            return contentCart;
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
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        public async Task<Cart> GetCartAsync(long userId)
        {
            var itemsInCart = await this.GetContentAsync(userId).ConfigureAwait(false);

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
    }
}

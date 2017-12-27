namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AutoMapper;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<ContentClassForUnitTest, ContentCart>
    {
        private readonly ICartRepository<ContentCartDto> repositoryCart;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentRepo">instance repository</param>
        public CartService(ICartRepository<ContentCartDto> contentRepo)
        {
            this.repositoryCart = contentRepo;
        }

        /// <summary>
        /// Creating and adding a new ContentCart object to Cart
        /// </summary>
        /// <param name="content">new ContentCart object</param>
        /// <param name="userId">id creator</param>
        /// <returns>saved ContentCart object in Cart</returns>
        public ContentCart AddNewContentInCart(ContentClassForUnitTest content, ulong userId)
        {
           // Mapping object Entity to object ContentCart
            var obj = Mapper.Map<ContentCart>(content);

            // Initialize CreatorId
            obj.CreatorId = userId;

            // Mapping object ContentCart to ContentCartDto
            var objContent = Mapper.Map<ContentCartDto>(obj);

            // Save object ContentCartDto in repository
            var objDto = this.repositoryCart.Add(objContent);

            // If the object is not added to the database
            // return null
            if (objDto == null)
            {
                return null;
            }

            // Mapping object ContentCartDto to ContentCart
            return Mapper.Map<ContentCart>(objDto);
        }

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>amount of object that deleted</returns>
        public int DeleteAllContentFromCart(ulong userId)
        {
            int flag = 0;
            var listDelete = this.repositoryCart.Find(item => item.CreatorId == userId);
            foreach (ContentCartDto item in listDelete)
            {
                var objRezalt = this.repositoryCart.Delete(item.Id);
                if (objRezalt != null)
                {
                    flag++;
                }
            }

            return flag;
        }

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>collection of remote objects</returns>
        public ICollection<ContentCartDto> DeleteContentFromCart(ulong userId)
        {
            var listDelete = this.repositoryCart.Find(item => item.CreatorId == userId
                && item.IsChecked == true);
            var listRezalt = new Collection<ContentCartDto>();
            foreach (ContentCartDto item in listDelete)
            {
                var obj = this.repositoryCart.Delete(item.Id);
                if (obj != null)
                {
                    listRezalt.Add(obj);
                }
            }

            return listRezalt;
        }

        /// <summary>
        /// Method for check object CounterId as Bought
        /// </summary>
        /// <param name="contentId">contents id</param>
        /// <param name="userId">user id</param>
        /// <returns>changes object or </returns>
        public ContentCart SetContentAsBought(ulong contentId, ulong userId)
        {
            // get object ContentCartDto according to the methods parameters
            var itemSearch = this.repositoryCart
                .Find(item => item.Id == contentId & item.CreatorId == userId).FirstOrDefault();

            // verify for null rezalt
            if (itemSearch == null)
            {
                return null;
            }

            itemSearch.IsBought = true;
            return Mapper.Map<ContentCart>(itemSearch);
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="id">content identificator</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        public bool FindContentInCart(ulong id) => this.repositoryCart
            .Get(id) != null;

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCart> GetItemsInCart(ulong id)
        {
            var itemsInCart = this.repositoryCart.Find(x => x.Id == id);
            IList<ContentCart> itemsDto = new List<ContentCart>();
            foreach (ContentCartDto itemDto in itemsInCart)
            {
                itemsDto.Add(Mapper.Map<ContentCart>(itemDto));
            }

            return itemsDto;
        }

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="contentId">id content</param>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>if object state is checked return true
        /// else return false</returns>
        public bool CheckedContent(ulong contentId, ulong userId)
        {
            var objectForChecked = this.repositoryCart.CheckedContent(item => item.Id == contentId
            && item.CreatorId == userId);
            return objectForChecked.IsChecked == true;
        }

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="contentId">id content</param>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>if object state is unchecked return true
        /// else return false</returns>
        public bool UnCheckedContent(ulong contentId, ulong userId)
        {
            var objectForChecked = this.repositoryCart.CheckedContent(item => item.Id == contentId
            && item.CreatorId == userId);
            return objectForChecked.IsChecked == false;
        }

        /// <summary>
        /// Get created Cart model object
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        public Cart GetCart(ulong userId)
        {
            var itemsInCart = this.GetItemsInCart(userId);
            var model = new Common.Models.Cart();
            model.ContentCartCollection = itemsInCart;
            model.CountItemsInCollection = this.GetCountItems(itemsInCart);
            model.PriceAllItemsCollection = this.GetPrice(itemsInCart);
            return model;
        }

        /// <summary>
        /// Get count items for User
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Count Items in cart</returns>
        public uint GetCountItems(ulong id)
        {
            var cart = this.GetItemsInCart(id);
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
            var cart = this.GetItemsInCart(id);
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

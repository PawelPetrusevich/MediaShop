namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<ContentClassForUnitTest, ContentCart>
    {
        private readonly ICartRespository<ContentCartDto> repositoryCart;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="contentRepo">instance repository</param>
        public CartService(ICartRespository<ContentCartDto> contentRepo)
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for deleting selected items
        /// </summary>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>collection of remote objects</returns>
        public IEnumerable<ContentCartDto> DeleteContentFromCart(ulong userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="id">content identificator</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        public bool FindContentInCart(ulong id) => this.repositoryCart
            .Get(id) != null;

        public Cart GetCart(ulong userid)
        {
            throw new NotImplementedException();
        }

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
        /// <returns>object that checked for
        /// the control his condition</returns>
        public ContentCart CheckedContent(ulong contentId, ulong userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="contentId">id content</param>
        /// <param name="userId">user id as identificator cart</param>
        /// <returns>object that checked for
        /// the control his condition</returns>
        public ContentCart UnCheckedContent(ulong contentId, ulong userId)
        {
            throw new NotImplementedException();
        }

        public uint GetCountItems(ulong id)
        {
            throw new NotImplementedException();
        }

        public uint GetCountItems(IEnumerable<ContentCart> cart)
        {
            throw new NotImplementedException();
        }

        public float GetPrice(ulong id)
        {
            throw new NotImplementedException();
        }

        public float GetPrice(IEnumerable<ContentCart> cart)
        {
            throw new NotImplementedException();
        }
    }
}

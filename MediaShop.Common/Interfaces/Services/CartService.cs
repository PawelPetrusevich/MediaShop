namespace MediaShop.Common.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;

    /// <summary>
    /// Service for work with cart
    /// </summary>
    public class CartService : ICartService<Entity, ContentCart>
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
        /// <returns>saved ContentCart object in Cart</returns>
        public ContentCart AddNewContentInCart(Entity content)
        {
            // Configuration Mapper ???
            var mapperConfig = new MapperConfiguration(s => s.AddProfile(new MapperProfile()));
            var mapper = new Mapper(mapperConfig);

            // Mapping object Entoty to object ContentCart
            var obj = Mapper.Map<ContentCart>(content);

            // Mapping object ContentCart to ContentCartDto
            var objContent = Mapper.Map<ContentCartDto>(obj);

            // Save object ContentCartDto in repository
            var objDto = this.repositoryCart.Add(objContent);

            if (objDto == null)
            {
                return null;
            }

            // Mapping object ContentCartDto to ContentCart
            return Mapper.Map<ContentCart>(objDto);
        }

        /// <summary>
        /// Checking the existence of content in cart
        /// </summary>
        /// <param name="id">content identificator</param>
        /// <returns>true - content exist in cart
        /// false - content does not exist in cart</returns>
        public bool FindContentInCart(int id) => this.repositoryCart
            .Find(identificator => identificator.ContentId == id)
            .FirstOrDefault() == null;

        public Cart GetCart(int id)
        {
            throw new NotImplementedException();
        }

        public uint GetCountItems(int id)
        {
            throw new NotImplementedException();
        }

        public uint GetCountItems(IEnumerable<ContentCart> cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find items in a cart by user Id and return a item collection
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns> shopping cart for a user </returns>
        public IEnumerable<ContentCart> GetItemsInCart(int id)
        {
            var itemsInCart = this.repositoryCart.Find(x => x.Id == id);
            IList<ContentCart> itemsDto = new List<ContentCart>();
            foreach (ContentCartDto itemDto in itemsInCart)
            {
                itemsDto.Add(Mapper.Map<ContentCart>(itemDto));
            }

            return itemsDto;
        }

        public float GetPrice(int id)
        {
            throw new NotImplementedException();
        }

        public float GetPrice(IEnumerable<ContentCart> cart)
        {
            throw new NotImplementedException();
        }
    }
}

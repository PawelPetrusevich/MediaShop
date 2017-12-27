namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Context;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class CartRepository : ICartRepository<ContentCartDto>
    {
        /// <summary>
        /// Method for add object type ContentCartDto
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Add(ContentCartDto model)
        {
            using (var contentCartContext = new MediaContext())
            {
                var result = contentCartContext.ContentCart.Add(model);
                contentCartContext.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// Method for update object type ContentCartDto
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Update(ContentCartDto model)
        {
            using (var contextContentCart = new MediaContext())
            {
                var contentCart = contextContentCart.ContentCart.Find(model.Id);
                contentCart = model;
                contextContentCart.SaveChanges();
                return contentCart;
            }
        }

        /// <summary>
        /// Method for delete object type ContentCartDto
        /// </summary>
        /// <param name="model">object for delete</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Delete(ContentCartDto model)
        {
            using (var contextContentCart = new MediaContext())
            {
                var result = contextContentCart.ContentCart.Remove(model);
                contextContentCart.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// Method for delete object type ContentCartDto
        /// </summary>
        /// <param name="id">id for delete</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Delete(ulong id)
        {
            using (var contentCartContext = new MediaContext())
            {
                var contentCart = contentCartContext.ContentCart.Where(x => x.Id == id).Single();
                var result = contentCartContext.ContentCart.Remove(contentCart);
                contentCartContext.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// Mefod for delete all content from the carts repository
        /// </summary>
        /// <param name="userId">identifier carts owner</param>
        /// <returns>amount is remote content in repository</returns>
        public int DeleteAll(ulong userId)
        {
            using (var contentCartContext = new MediaContext())
            {
                var contentCartCollection = contentCartContext.ContentCart.Where(x => x.CreatorId == userId);
                if (contentCartCollection != null)
                {
                    foreach (var contentCart in contentCartCollection)
                    {
                        var result = contentCartContext.ContentCart.Remove(contentCart);
                    }

                    contentCartContext.SaveChanges();
                }

                return contentCartCollection.Count();
            }
        }

        /// <summary>
        /// Method for find collection of object type ContentCartDto
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        public IEnumerable<ContentCartDto> Find(Expression<Func<ContentCartDto, bool>> filter)
        {
            using (var contentCartContext = new MediaContext())
            {
                var result = contentCartContext.ContentCart.Where(filter);
                return result;
            }
        }

        /// <summary>
        /// Method for getting object type ContentCartDto
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Get(ulong id)
        {
            using (var contentCartContext = new MediaContext())
            {
                var contentCart = contentCartContext.ContentCart.Where(x => x.Id == id).SingleOrDefault();
                return contentCart;
            }
        }
    }
}

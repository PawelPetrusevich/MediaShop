namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class CartRepository : ICartRespository<ContentCartDto>
    {
        /// <summary>
        /// Method for updating object type ContentCartDto
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Add(ContentCartDto model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for updating object type ContentCartDto
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Delete(ContentCartDto model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for deleting object type ContentCartDto
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Delete(ulong id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mefod for delete all content from the carts repository
        /// </summary>
        /// <param name="userId">identifier carts owner</param>
        /// <returns>amount is remote content in repository</returns>
        public int DeleteAll(ulong userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for find collection of object type ContentCartDto
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        public IEnumerable<ContentCartDto> Find(Expression<Func<ContentCartDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for getting object type ContentCartDto
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public ContentCartDto Get(ulong id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>object that checked for
        /// the control his condition</returns>
        public ContentCartDto CheckedContent(Expression<Func<ContentCartDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to indicate the ContentCart
        /// object as selected for deletion
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>object that checked for
        /// the control his condition</returns>
        public ContentCartDto UnCheckedContent(Expression<Func<ContentCartDto, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}

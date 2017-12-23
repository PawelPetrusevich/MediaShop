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
        public ContentCartDto Delete(int id)
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
        public ContentCartDto Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

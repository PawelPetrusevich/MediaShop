namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the repository when working with the ShoppingCart
    /// </summary>
    public interface ICartRepository : IRepository<ContentCart>
    {
        /// <summary>
        /// Method for getting collection objects type ContentCart in state InCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        IEnumerable<ContentCart> GetById(long userId);

        /// <summary>
        /// Method for getting collection objects type ContentCartDto
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        IEnumerable<ContentCart> GetAll(long userId);
    }
}

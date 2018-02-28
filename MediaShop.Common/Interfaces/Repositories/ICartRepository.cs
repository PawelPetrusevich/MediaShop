namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface describing the methods of
    /// interaction with the repository when working with the ShoppingCart
    /// </summary>
    public interface ICartRepository : IRepository<ContentCart>, IRepositoryAsync<ContentCart>
    {
        /// <summary>
        /// Method for getting object type ContentCart
        /// by Id
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        Task<ContentCart> GetAsync(long id);

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="model">object for delete</param>
        /// <returns>rezalt operation</returns>
        Task<int> DeleteAsync(ContentCart model);

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="id">id for delete</param>
        /// <returns>rezalt operation</returns>
        Task<int> DeleteAsync(long id);

        /// <summary>
        /// Method for getting collection objects type ContentCart in state InCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        IEnumerable<ContentCart> GetById(long userId);

        /// <summary>
        /// Method for getting collection objects type ContentCart in state InCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        Task<IEnumerable<ContentCart>> GetByIdAsync(long userId);

        /// <summary>
        /// Method for getting collection objects type ContentCartDto
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        IEnumerable<ContentCart> GetAll(long userId);
    }
}

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models;

    /// <summary>
    /// Base interface for working with repository
    /// </summary>
    /// <typeparam name="TModel">type object</typeparam>
    public interface IRepository<TModel>
        where TModel : Entity
    {
        /// <summary>
        /// Method for getting object type TModel
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Get(ulong id);

        /// <summary>
        /// Method for additing object type TModel
        /// </summary>
        /// <param name="model">new object</param>
        /// <returns>rezalt operation</returns>
        TModel Add(TModel model);

        /// <summary>
        /// Method for updating object type TModel
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        TModel Update(TModel model);

        /// <summary>
        /// Method for deleting object type TModel
        /// </summary>
        /// <param name="model">delete object</param>
        /// <returns>rezalt operation</returns>
        TModel Delete(TModel model);

        /// <summary>
        /// Method for deleting object type TModel
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Delete(ulong id);

        /// <summary>
        /// Method for find collection of object type TModel
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}
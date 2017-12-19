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
    /// <typeparam name="TModel">Type content in Cart</typeparam>
    public interface ICartRespository<TModel>
        where TModel : ContentCart
    {
        /// <summary>
        /// Method for getting object type ContentCart
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Get(int id);

        /// <summary>
        /// Method for updating object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        TModel Add(TModel model);

        /// <summary>
        /// Method for updating object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        TModel Delete(TModel model);

        /// <summary>
        /// Method for deleting object type ContentCart
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Delete(int id);

        /// <summary>
        /// Method for find collection of object type ContentCart
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}

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
    public interface ICartRepository<TModel>
        where TModel : ContentCartDto
    {
        /// <summary>
        /// Method for getting object type ContentCart
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Get(ulong id);

        /// <summary>
        /// Method for add object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        TModel Add(TModel model);

        /// <summary>
        /// Method for update object type ContentCart
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        TModel Update(TModel model);

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="id">contents identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Delete(TModel id);

        /// <summary>
        /// Method for delete object type ContentCart
        /// </summary>
        /// <param name="id">contents identificator</param>
        /// <returns>rezalt operation</returns>
        TModel Delete(ulong id);

        /// <summary>
        /// Method for find collection of object type ContentCart
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}

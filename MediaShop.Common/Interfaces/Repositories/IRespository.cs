using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Models;

namespace MediaShop.Common.Interfaces.Repositories
{
    /// <summary>
    /// interface IRespository
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IRespository<TModel> where TModel : Entity
    {
        /// <summary>
        /// interface method get for type TModel
        /// </summary>
        /// <param name="id"></param> id model
        /// <returns></returns> tmodel
        TModel Get(int id);

        /// <summary>
        /// interface method add for type TModel
        /// </summary>
        /// <param name="id"></param> id
        /// <param name="model"></param>tmodel
        /// model
        /// <returns></returns>tmodel
        TModel Add(TModel model);

        /// <summary>
        ///  interface method update for type TModel
        /// </summary>
        /// <param name="model"></param>tmodel
        /// <returns></returns>tmodel
        TModel Update(TModel model);

        /// <summary>
        /// interface method delete for type TModel by model
        /// </summary>
        /// <param name="model"></param>tmodel
        /// <returns></returns>tmodel
        TModel Delete(TModel model);

        /// <summary>
        /// interface method delete for type TModel by id
        /// </summary>
        /// <param name="id"></param> id model
        /// <returns></returns>tmodel
        TModel Delete(int id);

        /// <summary>
        /// interface method find for type TModel by id
        /// </summary>
        /// <param name="filter"></param> lambda expression
        /// <returns></returns>tmodel
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}
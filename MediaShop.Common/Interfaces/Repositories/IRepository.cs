// <copyright file="IRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models;

    /// <summary>
    /// Interface IRepository
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    public interface IRepository<TModel> : IDisposable
        where TModel : Entity
    {
        /// <summary>
        /// Interface method get for type TModel
        /// </summary>
        /// <param name="id">Id of searching element</param>
        /// <returns>TModel</returns>
        TModel Get(long id);

        /// <summary>
        /// interface method add for type TModel
        /// </summary>
        /// <param name="model">Model to add</param>
        /// <returns>Added Model</returns>
        TModel Add(TModel model);

        /// <summary>
        ///  interface method update for type TModel
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>Updated model</returns>
        TModel Update(TModel model);

        /// <summary>
        /// interface method delete for type TModel by model
        /// </summary>
        /// <param name="model">Model to delete</param>
        /// <returns>Instance of deleted model</returns>
        TModel Delete(TModel model);

        /// <summary>
        /// interface method delete for type TModel by id
        /// </summary>
        /// <param name="id">Id of deleting model</param>
        /// <returns>Deleted model</returns>
        TModel Delete(long id);

        /// <summary>
        /// Find by id
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable entities</returns>
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}
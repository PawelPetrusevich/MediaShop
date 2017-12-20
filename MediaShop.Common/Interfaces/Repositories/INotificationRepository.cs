using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Repositories
{
    /// <summary>
    /// Interface of notifications' repository
    /// </summary>
    /// <typeparam name="TModel">Type of data stored in the repository</typeparam>
    public interface INotificationRepository<TModel>
        where TModel : Notification
    {
        /// <summary>
        /// Get notification
        /// </summary>
        /// <param name="id"> Notification identifier </param>
        /// <returns> Object of notification</returns>
        TModel Get(int id);

        /// <summary>
        /// Add notification
        /// </summary>
        /// <param name="model"> Notification object</param>
        /// <returns> Object of notification which was added </returns>
        TModel Add(TModel model);

        /// <summary>
        /// Update notification
        /// </summary>
        /// <param name="model"> Notification object </param>
        /// <returns> Object of notification which was changed </returns>
        TModel Update(TModel model);

        /// <summary>
        /// Delete notification by object
        /// </summary>
        /// <param name="model"> Notification object </param>
        /// <returns> Object of notification which was deleted </returns>
        TModel Delete(TModel model);

        /// <summary>
        /// Delete notification by identifier
        /// </summary>
        /// <param name="id">  Notification identifier </param>
        /// <returns> Object of notification which was deleted </returns>
        TModel Delete(int id);

        /// <summary>
        /// Find notifications in repository
        /// </summary>
        /// <param name="filter"> Filter for search</param>
        /// <returns> Items collection</returns>
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}
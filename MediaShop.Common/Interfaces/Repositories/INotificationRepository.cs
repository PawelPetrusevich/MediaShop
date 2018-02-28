using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Repositories
{
    /// <summary>
    /// Interface of notifications' repository
    /// </summary>
    public interface INotificationRepository : IRepository<Notification>, IRepositoryAsync<Notification>
    {
    }
}
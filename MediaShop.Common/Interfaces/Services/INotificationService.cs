﻿using MediaShop.Common.Dto;
using System.Collections.Generic;

namespace MediaShop.Common.Interfaces.Services
{
    /// <summary>
    /// Interface INotificationService
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Notify user
        /// </summary>
        /// <param name="notification">Notification and notification receiver</param>
        /// <returns>Notification</returns>
        NotificationDto Notify(NotificationDto notification);

        /// <summary>
        /// Get user notifications
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List notifications</returns>
        IEnumerable<NotificationDto> GetByUserId(long userId);
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;
using MediaShop.DataAccess.Properties;

namespace MediaShop.DataAccess.Repositories
{
    /// <summary>
    /// Repository subscribed users
    /// </summary>
    public class NotificationSubscribedUserRepository : Repository<NotificationSubscribedUser>, INotificationSubscribedUserRepository
    {
        public NotificationSubscribedUserRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get subscribed user devices
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List subscriber user devices</returns>
        public List<string> GetUserDeviceTokens(long userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(userId));
            }

            return DbSet.Where(entity => entity.UserId == userId).Select(n => n.DeviceIdentifier).ToList();
        }

        public async Task<List<string>> GetUserDeviceTokensAsync(long userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(userId));
            }

            return await DbSet.Where(entity => entity.UserId == userId).Select(n => n.DeviceIdentifier).ToListAsync();
        }

        /// <summary>
        /// Check user device on subscribtion
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="deviceId">Device id</param>
        /// <returns>True - user device subscribed</returns>
        public bool IsExists(long userId, string deviceId)
        {
            if (userId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(deviceId));
            }

            return DbSet.Any(entity => entity.UserId == userId && entity.DeviceIdentifier == deviceId);
        }

        public async Task<bool> IsExistsAsync(long userId, string deviceId)
        {
            if (userId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(deviceId));
            }

            return await DbSet.AnyAsync(entity => entity.UserId == userId && entity.DeviceIdentifier == deviceId);
        }

        /// <summary>
        /// Fing subscribtion for user and device
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="deviceId">Device id</param>
        /// <returns>Notification subscribtion</returns>
        public NotificationSubscribedUser Get(long userId, string deviceId)
        {
            if (userId < 0)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(userId));
            }

            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(deviceId));
            }

            return DbSet.SingleOrDefault(entity => entity.UserId == userId && entity.DeviceIdentifier == deviceId);
        }

        public async Task<NotificationSubscribedUser> GetAsync(long userId, string deviceId)
        {
            if (userId < 0)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(userId));
            }

            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(deviceId));
            }

            return await DbSet.SingleOrDefaultAsync(entity => entity.UserId == userId && entity.DeviceIdentifier == deviceId);
        }
    }
}
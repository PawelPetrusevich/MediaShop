using System.Collections.Generic;
using System.Threading.Tasks;
using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface INotificationSubscribedUserRepository : IRepository<NotificationSubscribedUser>, IRepositoryAsync<NotificationSubscribedUser>
    {
        List<string> GetUserDeviceTokens(long userId);

        Task<List<string>> GetUserDeviceTokensAsync(long userId);

        bool IsExists(long userId, string deviceId);

        Task<bool> IsExistsAsync(long userId, string deviceId);

        NotificationSubscribedUser Get(long userId, string deviceId);

        Task<NotificationSubscribedUser> GetAsync(long userId, string deviceId);
    }
}
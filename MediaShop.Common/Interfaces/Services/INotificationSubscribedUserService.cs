using MediaShop.Common.Dto;

namespace MediaShop.Common.Interfaces.Services
{
    /// <summary>
    /// Interface INotificationSubscribedUserService
    /// </summary>
    public interface INotificationSubscribedUserService
    {
        /// <summary>
        /// Subscribe user to receive notifications
        /// </summary>
        /// <param name="subscribe">Subscribe parameters</param>
        /// <returns>Subscribtion info</returns>
        NotificationSubscribedUserDto Subscribe(NotificationSubscribedUserDto subscribe);

        /// <summary>
        /// Check user is subscribed
        /// </summary>
        /// <param name="subscribe">Subscribe parameters</param>
        /// <returns>true  - subscribed</returns>
        bool UserIsSubscribed(NotificationSubscribedUserDto subscribe);
    }
}
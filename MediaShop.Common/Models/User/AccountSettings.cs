using System.Text;

namespace MediaShop.Common.Models.User
{
    public class AccountSettings : Entity
    {
        public const string DEFAULT_TIME_ZONE_ID = "UTC";

        /// <summary>
        /// Identifier timezone of user, default value +0
        /// </summary>
        public string TimeZoneId { get; set; } = DEFAULT_TIME_ZONE_ID;

        /// <summary>
        /// Languae of userinterface
        /// </summary>
        public Language InterfaceLanguage { get; set; } = Language.Eng;

        /// <summary>
        /// Turn on/off notification
        /// </summary>
        public bool NotificationStatus { get; set; } = true;
    }
}
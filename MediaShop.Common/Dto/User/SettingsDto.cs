using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    public class SettingsDto
    {
        public string UserLogin { get; set; }

        public string TimeZoneId { get; set; }

        /// <summary>
        /// Languae of userinterface
        /// </summary>
        public Languages InterfaceLanguage { get; set; }

        /// <summary>
        /// Turn on/off notification
        /// </summary>
        public bool NotificationStatus { get; set; }
    }
}
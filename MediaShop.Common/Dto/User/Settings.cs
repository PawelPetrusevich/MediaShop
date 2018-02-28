using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User.Validators
{
    public class Settings 
    {
        /// <summary>
        /// Identifier timezone of user, default value +0
        /// </summary>
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
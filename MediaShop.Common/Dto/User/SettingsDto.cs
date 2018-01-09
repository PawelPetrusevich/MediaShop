using FluentValidation.Attributes;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    [Validator(typeof(SettingsValidator))]
    public class SettingsDto
    {
        public int UserId { get; set; }

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
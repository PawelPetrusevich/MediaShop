using System.Collections.Generic;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    public class AccountDomain : Entity
    {
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>The login.</value>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the profile identifier.
        /// </summary>
        /// <value>The profile identifier.</value>
        public long ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the settings identifier.
        /// </summary>
        /// <value>The settings identifier.</value>
        public long SettingsId { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>The profile.</value>
        public virtual ProfileBl Profile { get; set; } = new ProfileBl();

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual SettingsDomain Settings { get; set; } = new SettingsDomain();

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public virtual ICollection<Role> Permissions { get; set; } = new SortedSet<Role>();
    }
}
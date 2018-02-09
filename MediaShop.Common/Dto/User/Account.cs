using System.Collections.Generic;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    /// <summary>
    /// Account
    /// </summary>
    public class Account
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
        /// Get or sets flag isBanned
        /// </summary>
        /// <value>true - user is banned</value>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Get or sets flag isDeleted
        /// </summary>
        /// <value>true - user unregistered</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>The profile.</value>
        public Profile Profile { get; set; } = new Profile();

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public Settings Settings { get; set; } = new Settings();

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public Permissions Permissions { get; set; }
    }
}
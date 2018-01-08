// <copyright file="Account.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;

namespace MediaShop.Common.Models.User
{
    using System.Collections.Generic;

    /// <summary>
    /// Class Account.
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    public class Account : Entity
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
        public virtual AccountProfile Profile { get; set; } = new AccountProfile();

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual AccountSettings Settings { get; set; } = new AccountSettings();

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public virtual ICollection<Role> Permissions { get; set; } = new SortedSet<Role>();
    }
}
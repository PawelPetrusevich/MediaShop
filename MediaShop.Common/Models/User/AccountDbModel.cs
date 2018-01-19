// <copyright file="AccountDbModel.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;

namespace MediaShop.Common.Models.User
{
    using System.Collections.Generic;

    /// <summary>
    /// Class AccountDbModel.
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    public class AccountDbModel : Entity
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
        public bool IsBanned { get; set; } = false;

        /// <summary>
        /// Get or sets flag isDeleted
        /// </summary>
        /// <value>true - user unregistered</value>
        public bool IsDeleted { get; set; } = false;

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
        public virtual Profile Profile { get; set; } = new Profile();

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual Settings Settings { get; set; } = new Settings();

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission> { new Permission() };
    }
}
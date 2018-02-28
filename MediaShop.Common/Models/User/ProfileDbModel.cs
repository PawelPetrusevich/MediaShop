// <copyright file="Profiles.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models.User
{
    using System;

    /// <summary>
    /// Class ProfileDbModel.
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    public class ProfileDbModel : Entity
    {
        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }
    }
}
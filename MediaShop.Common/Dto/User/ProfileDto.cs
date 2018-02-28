// <copyright file="UserDto.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using FluentValidation.Attributes;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    using System;

    /// <summary>
    /// Class ProfileDto.
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    [Validator(typeof(ProfileValidator))]
    public class ProfileDto
    {
        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        public DateTime DateOfBirth { get; set; }

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

        /// <summary>
        /// Account  id
        /// </summary>
        public long AccountId { get; set; }
    }
}
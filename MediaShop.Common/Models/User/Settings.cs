// <copyright file="Settings.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;

namespace MediaShop.Common.Models.User
{
    /// <summary>
    /// Class describes personal user settings
    /// </summary>
    public class Settings : Entity
    {
        /// <summary>
        /// Identifier timezone of user, default value +0
        /// </summary>
        public string TimeZoneId { get; set; } = Constants.DefaultTimeZoneId;

        /// <summary>
        /// Languae of userinterface
        /// </summary>
        public Languages InterfaceLanguage { get; set; } = Languages.Eng;

        /// <summary>
        /// Turn on/off notification
        /// </summary>
        public bool NotificationStatus { get; set; } = true;

        /// <summary>
        /// reference to account table
        /// </summary>
        public virtual Account AccountOf { get; set; }

        /// <summary>
        /// id from table Account
        /// </summary>
        public long AccountId { get; set; }
    }
}
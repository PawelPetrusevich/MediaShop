// <copyright file="Settings.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Models.User
{
    /// <summary>
    /// Class describes personal user settings
    /// </summary>
    public class SettingsDbModel : Entity
    {
        /// <summary>
        /// Identifier timezone of user, default value +0
        /// </summary>
        public string TimeZoneId { get; set; } = Resources.DefaultTimeZoneId;

        /// <summary>
        /// Languae of userinterface
        /// </summary>
        public Languages InterfaceLanguage { get; set; } = Languages.Eng;

        /// <summary>
        /// Turn on/off notification
        /// </summary>
        public bool NotificationStatus { get; set; } = true;
    }
}
// <copyright file="Settings.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models.User
{
    /// <summary>
    /// Class describes personal user settings
    /// </summary>
    public class Settings : Entity
    {
        /// <summary>
        /// Default id for timezone UTC
        /// </summary>
        public const string DefaultTimeZoneId = "UTC";

        /// <summary>
        /// Identifier timezone of user, default value +0
        /// </summary>
        public string TimeZoneId { get; set; } = DefaultTimeZoneId;

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
        public ulong AccountId { get; set; }
    }
}
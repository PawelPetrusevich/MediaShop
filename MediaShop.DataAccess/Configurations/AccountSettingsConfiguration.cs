// <copyright file="AccountSettingsConfiguration.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class AccountSettingsConfiguration.
    /// </summary>
    public class AccountSettingsConfiguration : EntityTypeConfiguration<Settings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSettingsConfiguration"/> class.
        /// </summary>
        public AccountSettingsConfiguration()
        {
            this.HasKey(settings => settings.Id);
            this.HasRequired(c => c.AccountOf).WithRequiredDependent(p => p.Settings);
        }
    }
}
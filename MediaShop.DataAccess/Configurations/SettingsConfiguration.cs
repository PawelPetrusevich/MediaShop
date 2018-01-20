// <copyright file="SettingsConfiguration.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class SettingsConfiguration.
    /// </summary>
    public class SettingsConfiguration : EntityTypeConfiguration<SettingsDbModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsConfiguration"/> class.
        /// </summary>
        public SettingsConfiguration()
        {
            this.HasKey(settings => settings.Id);
        }
    }
}
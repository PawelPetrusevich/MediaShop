// <copyright file="ProfileConfiguration.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Configuration connect accountProfile with  table account
    /// </summary>
    public class ProfileConfiguration : EntityTypeConfiguration<ProfileDbModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileConfiguration"/> class.
        /// </summary>
        public ProfileConfiguration()
        {
            this.HasKey(profile => profile.Id);

            this.Property(p => p.FirstName).HasMaxLength(30);
            this.Property(p => p.LastName).HasMaxLength(30);
            this.Property(p => p.Phone).HasMaxLength(30);
        }
    }
}
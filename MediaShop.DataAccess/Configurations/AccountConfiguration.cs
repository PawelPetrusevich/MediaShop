// <copyright file="AccountConfiguration.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Common.Models.User;

    /// <summary>
    ///     Configuration connect account with other tables
    /// </summary>
    public class AccountConfiguration : EntityTypeConfiguration<AccountDbModel>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountConfiguration" /> class.
        /// </summary>
        public AccountConfiguration()
        {
            HasRequired(c => c.Profile);
            HasRequired(c => c.Settings);

            HasKey(p => p.Id);

            Property(p => p.Login).IsRequired();
            Property(p => p.Password).IsRequired();
            Property(p => p.Email).IsRequired().HasMaxLength(30);
        }
    }
}

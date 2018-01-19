// <copyright file="PermissionConfiguration.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    public class PermissionConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            HasRequired<AccountDbModel>(c => c.AccountDbModel).WithMany(x => x.Permissions);

            HasKey(p => p.Id);

            Property(p => p.Role).IsRequired();
        }
    }
}
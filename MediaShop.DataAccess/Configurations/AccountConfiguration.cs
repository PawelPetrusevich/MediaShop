﻿using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    public class AccountConfiguration : EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            this.HasRequired(c => c.Profile).WithRequiredPrincipal(p => p.AccountOf);

            this.HasKey(p => p.Id);

            this.Property(p => p.Login).IsRequired();
            this.Property(p => p.Password).IsRequired();
        }
    }
}
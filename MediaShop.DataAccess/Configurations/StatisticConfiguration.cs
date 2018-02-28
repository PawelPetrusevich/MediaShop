// <copyright file="StatisticConfiguration.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    public class StatisticConfiguration : EntityTypeConfiguration<StatisticDbModel>
    {
        public StatisticConfiguration()
        {
            HasRequired<AccountDbModel>(c => c.AccountDbModel)
                .WithMany(x => x.Statistics).HasForeignKey(k => k.AccountId);
                
            HasKey(p => p.Id);

            Property(p => p.DateLogIn).IsRequired();
            Property(p => p.DateLogOut).IsOptional();
        }
    }
}
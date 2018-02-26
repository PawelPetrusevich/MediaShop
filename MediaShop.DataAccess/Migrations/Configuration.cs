using MediaShop.Common;

namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using MediaShop.Common.Helpers;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<MediaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MediaContext context)
        {
            // initializing database at first time
            if (!context.Accounts.Any())
            {
                context.Accounts.Add(
                    new AccountDbModel()
                    {
                        Email = "user@tut.by",
                        Login = "admin",
                        Password = "admin".GetHash(),
                        Permissions = Permissions.Delete | Permissions.Create | Permissions.See,
                        AccountConfirmationToken = TokenHelper.NewToken(),
                        IsConfirmed = true,
                        Profile = new ProfileDbModel(),
                        Settings = new SettingsDbModel(),
                        CreatedDate = DateTime.Now
                    });
                context.Accounts.Add(
                    new AccountDbModel()
                    {
                        Email = "seller@test.com",
                        Login = "seller",
                        Password = "seller".GetHash(),
                        Permissions = Permissions.Create | Permissions.See,
                        AccountConfirmationToken = TokenHelper.NewToken(),
                        IsConfirmed = true,
                        Profile = new ProfileDbModel(),
                        Settings = new SettingsDbModel(),
                        CreatedDate = DateTime.Now
                    });
                context.Accounts.Add(
                    new AccountDbModel()
                    {
                        Email = "user@test.com",
                        Login = "user",
                        Password = "user".GetHash(),
                        Permissions = Permissions.See,
                        AccountConfirmationToken = TokenHelper.NewToken(),
                        IsConfirmed = true,
                        Profile = new ProfileDbModel(),
                        Settings = new SettingsDbModel(),
                        CreatedDate = DateTime.Now
                    });
            }
        }
    }
}

// <copyright file="NInjectProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.User;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.DataAccess
{
    using System.Data.Entity;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.Content;
    using MediaShop.DataAccess.Context;
    using MediaShop.DataAccess.Repositories;

    using Ninject.Modules;

    /// <summary>
    /// Class NInjectProfile.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class NInjectProfile : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<ICartRepository>().To<CartRepository>();
            this.Bind<IProductRepository>().To<ProductRepository>();
            this.Bind<MediaContext>().ToSelf();
            this.Bind<IAccountRepository>().To<AccountRepository>();
            this.Bind<IProfileRepository>().To<ProfileRepository>();
            this.Bind<ISettingsRepository>().To<SettingsRepository>();
            this.Bind<IStatisticRepository>().To<StatisticRepository>();
            this.Bind<DbContext>().To<MediaContext>();
            this.Bind<INotificationRepository>().To<NotificationRepository>();
            this.Bind<INotificationSubscribedUserRepository>().To<NotificationSubscribedUserRepository>();
            this.Bind<IAccountFactoryRepository>().To<AccountFactoryRepository>();
            this.Bind<IUserFactoryRepository>().To<UserFactoryRepository>();
            this.Bind<IPayPalPaymentRepository>().To<PayPalPaymentRepository>();
            this.Bind<IDefrayalRepository>().To<DefrayalRepository>();
        }
    }
}

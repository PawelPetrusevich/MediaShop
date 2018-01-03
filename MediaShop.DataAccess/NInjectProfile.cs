// <copyright file="NInjectProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess
{
    using MediaShop.Common.Interfaces.Repositories;
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
            this.Bind<MediaContext>().ToSelf();
            this.Bind<IAccountRepository>().To<AccountRepository>();
            this.Bind<IAccountProfileRepository>().To<AccountProfileRepository>();
            this.Bind<IAccountSettingsRepository>().To<AccountSettingsRepository>();
        }
    }
}

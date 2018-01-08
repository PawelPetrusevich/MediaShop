// <copyright file="NInjectProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.BusinessLogic
{
    using MediaShop.BusinessLogic.Services;
    using MediaShop.Common.Interfaces.Services;
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
            Bind<IUserService>().To<UserService>();
            Bind<IProductService>().To<ProductService>();
        }
    }
}

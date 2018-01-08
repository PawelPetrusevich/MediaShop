﻿// <copyright file="NInjectProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.WebApi
{
    using Ninject.Modules;

    /// <summary>
    /// внедрение зависимостей
    /// </summary>
    public class NInjectProfile : NinjectModule
    {
        /// <summary>
        /// Load
        /// </summary>
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
        }
    }
}

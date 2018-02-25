// <copyright file="NInjectProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Interfaces;
using MediaShop.Common.Interfaces.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

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
            Bind<IHubContext<INotificationProxy>>().ToMethod((c) => GlobalHost.ConnectionManager.GetHubContext<NotificationHub, INotificationProxy>());
        }
    }
}

// <copyright file="NInjectProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.BusinessLogic
{
    using FluentValidation;

    using MediaShop.BusinessLogic.Services;
    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Dto.User.Validators;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
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
            Bind<IAccountService>().To<AccountService>();
            Bind<ISettingsService>().To<SettingsService>();
            Bind<IProfileService>().To<ProfileService>();
            Bind<INotificationService>().To<NotificationService>();
            Bind<IEmailService>().To<EmailService>();
            Bind<ICartService<ContentCartDto>>().To<CartService>();
            Bind<AbstractValidator<RegisterUserDto>>().To<ExistingUserValidator>();
        }
    }
}

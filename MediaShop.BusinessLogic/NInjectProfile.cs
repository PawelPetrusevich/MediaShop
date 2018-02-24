// <copyright file="NInjectProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using MailKit;
using MailKit.Net.Smtp;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.Messaging.Validators;
using MediaShop.Common.Helpers;
using MediaShop.Common.Interfaces;

namespace MediaShop.BusinessLogic
{
    using FluentValidation;

    using MediaShop.BusinessLogic.Services;
    using MediaShop.Common.Dto.Messaging;
    using MediaShop.Common.Dto.Messaging.Validators;
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
            Bind<IUserService>().To<UserService>();
            Bind<IPermissionService>().To<PermissionService>();
            Bind<INotificationService>().To<NotificationService>();
            Bind<IEmailService>().To<EmailService>();
            Bind<IValidator<AccountConfirmationDto>>().To<ExtAccountConfirmationValidator>();
            Bind<IValidator<ResetPasswordDto>>().To<ExtAccountPwdRestoreValidator>();
            Bind<IAccountTokenFactoryValidator>().To<AccountTokenFactoryValidator>();
            Bind<ICartService<ContentCartDto>>().To<CartService>();
            Bind<IValidator<RegisterUserDto>>().To<ExistingUserValidator>();
            Bind<IPayPalPaymentService>().To<PayPalPaymentService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IBannedService>().To<BannedService>();
            Bind<IValidator<NotificationDto>>().To<NotificationDtoValidator>();
            Bind<IEmailSettingsConfig>().ToMethod(context => EmailSettingsConfigHelper.InitWithAppConf());
            Bind<IMailService>().To<SmtpClient>();
        }
    }
}

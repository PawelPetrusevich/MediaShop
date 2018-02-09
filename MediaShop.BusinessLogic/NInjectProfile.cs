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
using MediaShop.Common.Helpers;
using MediaShop.Common.Interfaces;

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
            Bind<IValidator<RegisterUserDto>>().To<ExistingUserValidator>();
            Bind<IPaymentService>().To<PaymentService>();
            Bind<IProductService>().To<ProductService>();

            var email = ConfigurationManager.AppSettings["AppEmail"];
            var emailPvd = ConfigurationManager.AppSettings["AppEmailPassword"];
            var emailSmtpHost = ConfigurationManager.AppSettings["AppEmailSmtpHost"];
            var emailSmtpPort = int.Parse(ConfigurationManager.AppSettings["AppEmailSmtpPort"]);
            var serverSchema = ConfigurationManager.AppSettings["ServerSchema"];
            var serverHost = ConfigurationManager.AppSettings["ServerHost"];
            var serverPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);
            var servirUri = new UriBuilder(serverSchema, serverHost, serverPort);

            var temapltesPath = new Dictionary<string, string>();
            var pathFolders = AppContext.BaseDirectory.Split('\\').ToList();
            pathFolders[0] += '\\';
            pathFolders = pathFolders.Take(pathFolders.Count - 3).ToList();
            pathFolders.Add("MediaShop.WebApi");
            pathFolders.Add("Content");
            pathFolders.Add("Templates");
            var templatesFoldePath = Path.Combine(pathFolders.ToArray());
            temapltesPath.Add("AccountConfirmationEmailTemplate", Path.Combine(templatesFoldePath, "AccountConfirmationEmailTemplate.html"));

            Bind<IEmailSettingsConfig>().To<EmailSettingsConfig>().WithConstructorArgument("login", email)
                .WithConstructorArgument("pwd", emailPvd)
                .WithConstructorArgument("smtpHost", emailSmtpHost)
                .WithConstructorArgument("smtpPort", emailSmtpPort)
                .WithConstructorArgument("webApiUri", servirUri)
                .WithConstructorArgument("tempaltesLocations", temapltesPath);

            Bind<IMailService>().To<SmtpClient>();
        }
    }
}

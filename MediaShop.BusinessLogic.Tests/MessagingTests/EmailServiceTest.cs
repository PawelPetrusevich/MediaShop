using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using MediaShop.Common;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;
using MailKit;
using MailKit.Net.Smtp;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Helpers;
using MediaShop.Common.Interfaces;
using MimeKit;

namespace MediaShop.BusinessLogic.Tests.MessagingTests
{
    [TestFixture]
    public class EmailServiceTest
    {
        private Mock<SmtpClient> _smtpClientMock;

        private EmailService _mailService;

        private AccountConfirmationDto _confirmDto;

        private AccountPwdRestoreDto _restoreDto;

        public EmailServiceTest()
        {
            Mapper.Reset();
            Mapper.Initialize(x => { x.AddProfile<MapperProfile>(); });
        }

        [SetUp]
        public void Initialize()
        {
            _smtpClientMock = new Mock<SmtpClient>();
            var temapltesPath = new Dictionary<string, string>();
            var pathFolders = AppContext.BaseDirectory.Split('\\').ToList();
            if (string.IsNullOrWhiteSpace(pathFolders[pathFolders.Count - 1]))
                pathFolders = pathFolders.Take(pathFolders.Count - 1).ToList();
            pathFolders[0] += '\\';
            pathFolders = pathFolders.Take(pathFolders.Count - 3).ToList();
            pathFolders.Add("MediaShop.WebApi");
            pathFolders.Add("Content");
            pathFolders.Add("Templates");
            var templatesFoldePath = Path.Combine(pathFolders.ToArray());
            temapltesPath.Add("AccountConfirmationEmailTemplate", Path.Combine(templatesFoldePath, "AccountConfirmationEmailTemplate.html"));
            temapltesPath.Add("AccountPwdRestoreEmailTemplate", Path.Combine(templatesFoldePath, "AccountPwdRestoreEmailTemplate.html"));
            
            IEmailSettingsConfig emailConf = new EmailSettingsConfig("smtp.gmail.com", 587, "noreply.mediashop@gmail.com", "ayTYh?2-3xtUB26j", temapltesPath);
            _mailService = new EmailService(_smtpClientMock.Object, emailConf);
            _confirmDto = new AccountConfirmationDto()
            {
                Token = TokenHelper.NewToken(),
                Email = "noreply.mediashop@gmail.com"
            };

            _restoreDto = new AccountPwdRestoreDto()
            {
                Token = TokenHelper.NewToken(),
                Email = "noreply.mediashop@gmail.com"
            };
        }


        [Test]
        public void SendConfirmationTest()
        {
            int callCount = 0;
            _smtpClientMock.Setup(x =>
                x.Send(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>())).Callback(
                () => { callCount++; });
            _mailService.SendConfirmation(_confirmDto);

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void SendPwdRestoreTest()
        {
            int callCount = 0;
            _smtpClientMock.Setup(x =>
                x.Send(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>())).Callback(
                () => { callCount++; });
            _mailService.SendRestorePwdLink(_restoreDto);

            Assert.AreEqual(1, callCount);
        }
    }
}
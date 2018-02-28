using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common;
using MediaShop.Common.Dto;
using MediaShop.Common.Helpers;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using Moq;
using NUnit.Framework;
using BLResources = MediaShop.BusinessLogic.Properties.Resources;
using MediaShop.Common.Dto.Messaging;
using FluentValidation;
using MediaShop.Common.Dto.Messaging.Validators;
using MediaShop.Common.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MediaShop.BusinessLogic.Tests.MessagingTests
{
    [TestFixture]
    public class NotificationServiceTest
    {
        private Mock<INotificationRepository> _notificationRepoMock;
        private Mock<INotificationSubscribedUserRepository> _notificationSubscrubedUserMock;
        private Mock<IHubContext<INotificationProxy>> _hubMock;


        private NotificationSubscribedUserDto _subscribeDataDto;
        private NotificationSubscribedUser _subscribeData;

        private Notification _notification;
        private NotificationDto _notificationDto;

        private NotificationService _service;
        private List<string> _testTokens;

        private NotificationDtoValidator _validator;

        public NotificationServiceTest()
        {
            Mapper.Reset();
            Mapper.Initialize(x => { x.AddProfile<MapperProfile>(); });
        }

        [SetUp]
        public void Initialize()
        {
            _notificationSubscrubedUserMock = new Mock<INotificationSubscribedUserRepository>();
            _notificationRepoMock = new Mock<INotificationRepository>();
            _validator = new NotificationDtoValidator();

            _hubMock = new Mock<IHubContext<INotificationProxy>>();
            _hubMock.Setup(o => o.Clients.User(It.IsAny<string>()).UpdateNotices(It.IsAny<NotificationDto>()));

            _service = new NotificationService(_notificationSubscrubedUserMock.Object, _notificationRepoMock.Object, _validator, _hubMock.Object);
            _notification = new Notification()
            {
                CreatorId = 1,
                CreatedDate = DateTime.Now,
                Id = 1,
                Message = "test",
                ReceiverId = 1,
                Title = "test"
            };
            _notificationDto = new NotificationDto()
            {
                Message = "test",
                Title = "test",
                ReceiverId = 1,
                SenderId = 1
            };
            _testTokens = new List<string>();
            _testTokens.AddRange(Enumerable.Repeat("test", 5));
        }

        [Test]
        public void SendNotificationTest()
        {
            _notificationSubscrubedUserMock.Setup(r => r.GetUserDeviceTokens(It.IsAny<long>())).Returns(_testTokens);
            _notificationRepoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Returns(new List<Notification>());

            _notificationRepoMock.Setup(r => r.Add(It.IsAny<Notification>()))
                .Returns<Notification>(n => n);

            Assert.IsNotNull(_service.Notify(_notificationDto));
        }

        [Test]
        public void SendExistNotificationTest()
        {
            var notifications = new List<Notification>();
            notifications.Add(_notification);
            _notificationSubscrubedUserMock.Setup(r => r.GetUserDeviceTokens(It.IsAny<long>())).Returns(_testTokens);
            _notificationRepoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Returns(notifications);

            _notificationRepoMock.Setup(r => r.Add(It.IsAny<Notification>()))
                .Returns<Notification>(n => n);
            var notificationActual = _service.Notify(_notificationDto);

            Assert.IsNotNull(notificationActual);
            Assert.AreEqual(_notification.ReceiverId, notificationActual.ReceiverId);
            Assert.AreEqual(_notification.Message, notificationActual.Message);
            Assert.AreEqual(_notification.Title, notificationActual.Title);
        }

        [Test]
        public void SendNoTitleNotification()
        {
            _notificationSubscrubedUserMock.Setup(r => r.GetUserDeviceTokens(It.IsAny<long>())).Returns(_testTokens);
            _notificationRepoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Returns(new List<Notification>());

            _notificationRepoMock.Setup(r => r.Add(It.IsAny<Notification>()))
                .Returns<Notification>(n => n);
            var notificationActual = _service.Notify(new NotificationDto()
            {
                ReceiverId = 1,
                Message = "test",
                SenderId = 1
            });

            Assert.IsNotNull(notificationActual);
            Assert.AreEqual(1, notificationActual.ReceiverId);
            Assert.AreEqual("test", notificationActual.Message);
            Assert.AreEqual(BLResources.DefaultNotificationTitle, notificationActual.Title);
        }

        [Test]
        public void SendAddToCartNotification()
        {
            _notificationSubscrubedUserMock.Setup(r => r.GetUserDeviceTokens(It.IsAny<long>())).Returns(_testTokens);
            _notificationRepoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Returns(new List<Notification>());

            _notificationRepoMock.Setup(r => r.Add(It.IsAny<Notification>()))
                .Returns<Notification>(n => n);
            var notificationActual = _service.AddToCartNotify(new AddToCartNotifyDto()
            {
                ReceiverId = 1,
                ProductName = "test"
            });

            Assert.IsNotNull(notificationActual);
            Assert.AreEqual(1, notificationActual.ReceiverId);
            Assert.AreEqual(NotificationHelper.FormatAddProductToCartMessage("test"), notificationActual.Message);
            Assert.AreEqual(BLResources.DefaultNotificationTitle, notificationActual.Title);
        }

        [Test]
        public void NotifyArgumentValidationTest()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Notify(null));
            Assert.Throws<ArgumentException>(() => _service.Notify(new NotificationDto()));
            Assert.Throws<ArgumentException>(() =>
                _service.Notify(new NotificationDto() { ReceiverId = 0, Message = "test", SenderId = 1 }));
            Assert.Throws<ArgumentException>(() =>
                _service.Notify(new NotificationDto() { Message = string.Empty, ReceiverId = 1, SenderId = 1 }));
            Assert.Throws<ArgumentException>(() =>
                _service.Notify(new NotificationDto() { SenderId = 0, Message = "test", ReceiverId = 1 }));
        }
    }
}
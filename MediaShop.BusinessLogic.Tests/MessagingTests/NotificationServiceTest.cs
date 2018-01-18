using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.MessagingTests
{
    [TestFixture]
    public class NotificationServiceTest
    {
        private Mock<INotificationRepository> _notificationRepoMock;
        private Mock<INotificationSubscribedUserRepository> _notificationSubscrubedUserMock;

        private NotificationSubscribedUserDto _subscribeDataDto;
        private NotificationSubscribedUser _subscribeData;

        private Notification _notification;
        private NotificationDto _notificationDto;

        private NotificationService _service;
        private List<string> _testTokens;

        public NotificationServiceTest()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Notification, NotificationDto>().ReverseMap()
                    .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                    .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.SenderId));
                config.CreateMap<NotificationSubscribedUser, NotificationSubscribedUserDto>().ReverseMap()
                    .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                    .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.UserId));
            });
        }

        [SetUp]
        public void Initialize()
        {
            _notificationSubscrubedUserMock = new Mock<INotificationSubscribedUserRepository>();
            _notificationRepoMock = new Mock<INotificationRepository>();

            _service = new NotificationService(_notificationSubscrubedUserMock.Object, _notificationRepoMock.Object);
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
                .Returns(_notification);

            Assert.IsNotNull(_service.Notify(_notificationDto));
        }
    }
}
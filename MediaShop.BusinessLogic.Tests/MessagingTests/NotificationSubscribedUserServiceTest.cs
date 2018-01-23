using System;
using AutoMapper;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Notification;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.MessagingTests
{
    [TestFixture]
    public class NotificationSubscribedUserServiceTest
    {
        private Mock<INotificationSubscribedUserRepository> _subscribedUsersStoreMock;
        private NotificationSubscribedUserDto _subscribeDataDto;
        private NotificationSubscribedUser _subscribeData;
        private NotificationSubscribedUserService _service;

        public NotificationSubscribedUserServiceTest()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
                config.CreateMap<NotificationSubscribedUser, NotificationSubscribedUserDto>().ReverseMap()
                    .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                    .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.UserId)));
        }

        [SetUp]
        public void Initialize()
        {
            _subscribedUsersStoreMock = new Mock<INotificationSubscribedUserRepository>();
            _subscribeDataDto = new NotificationSubscribedUserDto()
            {
                DeviceIdentifier = "test",
                UserId = 1
            };
            _service = new NotificationSubscribedUserService(_subscribedUsersStoreMock.Object);
            _subscribeData = new NotificationSubscribedUser()
            {
                UserId = 1,
                DeviceIdentifier = "test",
                CreatedDate = DateTime.Now,
                CreatorId = 1
            };
        }

        [Test]
        public void SubscribeUserTest()
        {
            _subscribedUsersStoreMock.Setup(x => x.Add(It.IsAny<NotificationSubscribedUser>()))
                .Returns(_subscribeData);
            _subscribedUsersStoreMock.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
                .Returns((NotificationSubscribedUser) null);

            Assert.IsNotNull(_service.Subscribe(_subscribeDataDto));
        }

        [Test]
        public void SubscribeSubscribedUserTest()
        {
            _subscribedUsersStoreMock.Setup(x => x.Add(It.IsAny<NotificationSubscribedUser>()))
                .Returns(_subscribeData);
            _subscribedUsersStoreMock.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(_subscribeData);

            var subscribResult = _service.Subscribe(_subscribeDataDto);

            Assert.AreEqual(_subscribeData.DeviceIdentifier, subscribResult.DeviceIdentifier);
            Assert.AreEqual(_subscribeData.UserId, subscribResult.UserId);
        }

        [Test]
        public void UserIsSubscribedTest()
        {
            _subscribedUsersStoreMock.Setup(x => x.Add(It.IsAny<NotificationSubscribedUser>()))
                .Returns(_subscribeData);
            _subscribedUsersStoreMock.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(_subscribeData);

            var subscribResult = _service.UserIsSubscribed(_subscribeDataDto);

            Assert.AreEqual(true, subscribResult);

            _subscribedUsersStoreMock.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
                .Returns((NotificationSubscribedUser) null);

            var subscribResult2 = _service.UserIsSubscribed(_subscribeDataDto);

            Assert.AreEqual(false, subscribResult2);
        }

        [Test]
        public void SubscribeArgumentValidationTest()
        {
            Assert.Throws<ArgumentException>(() => _service.Subscribe(null));
            Assert.Throws<ArgumentException>(() => _service.Subscribe(new NotificationSubscribedUserDto()));
            Assert.Throws<ArgumentException>(() =>
                _service.Subscribe(new NotificationSubscribedUserDto() {UserId = 1}));
            Assert.Throws<ArgumentException>(() =>
                _service.Subscribe(new NotificationSubscribedUserDto() {DeviceIdentifier = string.Empty}));
            Assert.Throws<ArgumentException>(() =>
                _service.Subscribe(new NotificationSubscribedUserDto() {DeviceIdentifier = "test"}));
        }

        [Test]
        public void UserIsSubscribedArgumentValidationTest()
        {
            Assert.Throws<ArgumentException>(() => _service.UserIsSubscribed(null));
            Assert.Throws<ArgumentException>(() => _service.UserIsSubscribed(new NotificationSubscribedUserDto()));
            Assert.Throws<ArgumentException>(() =>
                _service.UserIsSubscribed(new NotificationSubscribedUserDto() {UserId = 1}));
            Assert.Throws<ArgumentException>(() =>
                _service.UserIsSubscribed(new NotificationSubscribedUserDto() {DeviceIdentifier = string.Empty}));
            Assert.Throws<ArgumentException>(() =>
                _service.UserIsSubscribed(new NotificationSubscribedUserDto() {DeviceIdentifier = "test"}));
        }
    }
}
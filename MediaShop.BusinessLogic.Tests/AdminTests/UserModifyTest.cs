using System;
using AutoMapper;
using FluentValidation;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;
using Profile = MediaShop.Common.Dto.User.Profile;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture]
    public class UserModifyTest
    {

        private ProfileDto _userProfile;
        private SettingsDto _userSettings;
        private Mock<IUserFactoryRepository> _factoryRepository;

        public UserModifyTest()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.CreateMap<Account, AccountDbModel>();
                config.CreateMap<Profile, ProfileDbModel>();                
            });
        }

        [SetUp]
        public void Init()
        {
            var mockfactoryRepository = new Mock<IUserFactoryRepository>();

            _factoryRepository = mockfactoryRepository;

            _userProfile = new ProfileDto()
            {
                DateOfBirth = DateTime.Now,
                FirstName = "Irina",
                LastName = "Bey",
                Phone = "802612344566",
                AccountId = 0,
            };

            _userSettings = new SettingsDto()
            {
                AccountId = 0,
                TimeZoneId = "blablabla",
                InterfaceLanguage = Languages.Eng,
                NotificationStatus = true
            };
        }

        [Test]
        public void DeletSoftTestSuccess()
        {
            var account = new AccountDbModel();         

            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.SoftDelete(It.IsAny<long>())).Returns(account);
            var userService = new UserService(_factoryRepository.Object);

            Assert.IsNotNull( userService.SoftDeleteByUser(2));

        }

        [Test]
        public void DeletSoftIncorrectInputDataTest()
        {       
            var userService = new UserService(_factoryRepository.Object);

            Assert.Throws<ArgumentException>(() => userService.SoftDeleteByUser(0));
        }

        [Test]
        public void DeletSoftNotFoundUserTest()
        {
            var userService = new UserService(_factoryRepository.Object);

            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns((AccountDbModel)null);
            Assert.Throws<NotFoundUserException>(() => userService.SoftDeleteByUser(2));
        }

        [Test]
        public void GetUserInfoSuccessTest()
        {
            var account = new AccountDbModel();

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);

            Assert.IsNotNull(userService.GetUserInfo(2));
        }

        [Test]
        public void GetUserInfoIncorrectInputDataTest()
        {
            var userService = new UserService(_factoryRepository.Object);

            Assert.Throws<ArgumentException>(() => userService.GetUserInfo(0));
        }

        [Test]
        public void GetUserInfoNotFoundUserTest()
        {
            var userService = new UserService(_factoryRepository.Object);

            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns((AccountDbModel)null);
            Assert.Throws<NotFoundUserException>(() => userService.GetUserInfo(2));
        }

        [Test]
        public void ModifyProfileSuccessTest()
        {
            var account = new AccountDbModel(){Profile = new ProfileDbModel(), Settings = new SettingsDbModel()};

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns(account);

            Assert.IsNotNull(userService.ModifyProfile(_userProfile));
        }

        [Test]
        public void ModifyProfileNotFoundUserTest()
        {
            var account = new AccountDbModel() { Profile = new ProfileDbModel(), Settings = new SettingsDbModel() };

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns((AccountDbModel)null);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns(account);

            Assert.Throws<NotFoundUserException>(() => userService.ModifyProfile(_userProfile));
        }

        [Test]
        public void ModifyProfileNotUpdateUserProfileTest()
        {
            var account = new AccountDbModel() { Profile = new ProfileDbModel(), Settings = new SettingsDbModel() };

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns((AccountDbModel)null);

            Assert.Throws<UpdateAccountException>(() => userService.ModifyProfile(_userProfile));
        }

        [Test]
        public void ModifySettingsSuccessTest()
        {
            var account = new AccountDbModel() { Profile = new ProfileDbModel(), Settings = new SettingsDbModel() };

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns(account);

            Assert.IsNotNull(userService.ModifySettings(_userSettings));
        }

        [Test]
        public void ModifySettingsNotFoundUserTest()
        {
            var account = new AccountDbModel() { Profile = new ProfileDbModel(), Settings = new SettingsDbModel() };

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns((AccountDbModel)null);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns(account);

            Assert.Throws<NotFoundUserException>(() => userService.ModifySettings(_userSettings));
        }

        [Test]
        public void ModifyProfileNotUpdateUserSettingsTest()
        {
            var account = new AccountDbModel() { Profile = new ProfileDbModel(), Settings = new SettingsDbModel() };

            var userService = new UserService(_factoryRepository.Object);
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns((AccountDbModel)null);

            Assert.Throws<UpdateAccountException>(() => userService.ModifySettings(_userSettings));
        }
    }
}

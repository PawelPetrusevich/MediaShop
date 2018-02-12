using System;
using AutoMapper;
using FluentValidation;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture]
    public class LoginTest
    {
        private Mock<IAccountFactoryRepository> _factoryRepository;
        private Mock<IEmailService> _emailService;
        private Mock<IValidator<RegisterUserDto>> _validator;
        private LoginDto _data;

        public LoginTest()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.CreateMap<RegisterUserDto, AccountDbModel>();
                config.CreateMap<Account, AccountDbModel>();
            });
        }

        [SetUp]
        public void Init()
        {
            var mockfactoryRepository = new Mock<IAccountFactoryRepository>();
            var mockEmailService = new Mock<IEmailService>();
            var mockValidator = new Mock<IValidator<RegisterUserDto>>();

            _factoryRepository = mockfactoryRepository;
            _emailService = mockEmailService;
            _validator = mockValidator;            

            _data = new LoginDto()
            {
                Login = "userTest",
                Password = "password"
            };          
        }

        [Test]
        public void LoginSuccessful()
        {
            _factoryRepository.Setup(x => x.Statistics.Add(It.IsAny<StatisticDbModel>()))
                .Returns(new StatisticDbModel{AccountDbModel = new AccountDbModel()});
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>())).Returns(new AccountDbModel(){Password = "password" });

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.IsNotNull(userService.Login(_data));
        }

        [Test]
        public void LoginNotFoundUser()
        {
            _factoryRepository.Setup(x => x.Statistics.Add(It.IsAny<StatisticDbModel>()))
                .Returns(new StatisticDbModel{ AccountDbModel = new AccountDbModel() });
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>())).Returns((AccountDbModel)null);

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.Throws<NotFoundUserException>(() => userService.Login(_data));
        }

        [Test]
        public void LoginIncorrectPassword()
        {
            _factoryRepository.Setup(x => x.Statistics.Add(It.IsAny<StatisticDbModel>()))
                .Returns(new StatisticDbModel { AccountDbModel = new AccountDbModel() });
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>()))
                .Returns(new AccountDbModel {Password = "pass"});

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.Throws<IncorrectPasswordException>(() => userService.Login(_data));
        }

        [Test]
        public void LoginDataRepositoryException()
        {
            _factoryRepository.Setup(x => x.Statistics.Add(It.IsAny<StatisticDbModel>()))
                .Returns((StatisticDbModel)null);
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>()))
                .Returns(new AccountDbModel { Password = "password" });

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.Throws<AddStatisticException>(() => userService.Login(_data));
        }

        [Test]
        public void LoginNotvalidDataNull()
        {
            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            _data = null;

            Assert.Throws<ArgumentNullException>(() =>userService.Login(_data));
        }

        [Test]
        public void LoginNotvalidDataEmptyLogin()
        {
            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            _data.Login = String.Empty;

            Assert.Throws<ArgumentNullException>(() => userService.Login(_data));
        }

        [Test]
        public void LoginNotvalidDataEmptyPassword()
        {
            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            _data.Password = String.Empty;

            Assert.Throws<ArgumentNullException>(() => userService.Login(_data));
        }

    }
}
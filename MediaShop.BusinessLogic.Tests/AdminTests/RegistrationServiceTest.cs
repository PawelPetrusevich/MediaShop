using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;


namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    using System.Linq;

    using FluentValidation;

    using Services;
    using Common.Interfaces.Services;

    [TestFixture]
    public class RegistrationServiceTest
    {
        private RegisterUserDto _user;
        private Mock<IAccountFactoryRepository> _factoryRepository;
        private Mock<IEmailService> _emailService;
        private Mock<IValidator<RegisterUserDto>> _validator;
        private Mock<IAccountTokenFactoryValidator> _tokenValidatorMock;

        public RegistrationServiceTest()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.CreateMap<RegisterUserDto, AccountDbModel>();
                config.CreateMap<Account, AccountDbModel>();
                config.CreateMap<AccountDbModel, AccountConfirmationDto>().ForMember(item => item.Token, opt => opt.MapFrom(s => s.AccountConfirmationToken));

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
            _tokenValidatorMock = new Mock<IAccountTokenFactoryValidator>();

            _tokenValidatorMock.Setup(v => v.AccountPwdRestore.Validate(It.IsAny<AccountPwdRestoreDto>()).IsValid)
                .Returns(true);

            _tokenValidatorMock.Setup(v => v.AccountConfirmation.Validate(It.IsAny<AccountConfirmationDto>()).IsValid)
                .Returns(true);

            _user = new RegisterUserDto()
            {
                Login = "User",
                Password = "12345",
                ConfirmPassword = "12345",
                Email = "12345",
            };
        }

        [Test]
        public void TestRegistrationSuccessfull()
        {
            var profile = new ProfileDbModel { Id = 1 };
            var account = new AccountDbModel
            {
                Id = 2,
                Login = "Ivan",
                Password = "111",
                Profile = profile
            };

            _factoryRepository.Setup(x => x.Accounts.Add(It.IsAny<AccountDbModel>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Find(It.IsAny<Expression<Func<AccountDbModel, bool>>>()))
                .Returns((IEnumerable<AccountDbModel>)null);
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<AccountConfirmationDto>()));

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object, _tokenValidatorMock.Object);

            Assert.IsNotNull(userService.Register(_user));
        }

        [Test]
        public void TestExistingLogin()
        {
            _factoryRepository.Setup(x => x.Accounts.Add(It.IsAny<AccountDbModel>())).Returns(new AccountDbModel());
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>())).Returns(new AccountDbModel());
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(false);

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object, _tokenValidatorMock.Object);

            Assert.Throws<ExistingLoginException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFailInRepository()
        {
            _factoryRepository.Setup(x => x.Accounts.Add(It.IsAny<AccountDbModel>())).Returns((AccountDbModel)null);
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<AccountConfirmationDto>()));

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object, _tokenValidatorMock.Object);

            Assert.Throws<AddAccountException>(() => userService.Register(_user));
        }
        [Test]
        public void LogoutException()
        {
            _validator.Setup(v => v.Validate(It.IsAny<RegisterUserDto>()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<AccountConfirmationDto>()));

            _factoryRepository.Setup(x => x.Statistics.Find(It.IsAny<Expression<Func<StatisticDbModel, bool>>>()))
                .Returns(Enumerable.Empty<StatisticDbModel>());

            _factoryRepository.Setup(x => x.Statistics.Update(It.IsAny<StatisticDbModel>())).Returns(new StatisticDbModel());

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object, _tokenValidatorMock.Object);

            Assert.Throws<AddStatisticException>(() => userService.Logout(1));
        }
        [Test]
        public void LogoutSuccessful()
        {
            var id = 1;
            var profile = new ProfileDbModel { Id = 1 };
            var account = new AccountDbModel
            {
                Id = 1,
                Login = "Koala",
                Password = "123",
                Profile = profile
            };
            var statistic = new StatisticDbModel()
            { AccountId = 2, AccountDbModel = account, DateLogIn = DateTime.Now, DateLogOut = null };
            var statistics = new List<StatisticDbModel> { statistic };

            _validator.Setup(v => v.Validate(It.IsAny<RegisterUserDto>()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<AccountConfirmationDto>()));

            _factoryRepository.Setup(x => x.Statistics.Find(It.IsAny<Expression<Func<StatisticDbModel, bool>>>()))
                .Returns(statistics);

            _factoryRepository.Setup(x => x.Statistics.Update(It.IsAny<StatisticDbModel>())).Returns(statistic);

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object, _tokenValidatorMock.Object);

            Assert.IsNotNull(userService.Logout(1));
        }
    }
}


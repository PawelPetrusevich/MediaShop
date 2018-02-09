using System;
using AutoMapper;
using FluentValidation;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture]
    public class ValidateuserFromtokenTest
    {
        private Mock<IAccountFactoryRepository> _factoryRepository;
        private Mock<IEmailService> _emailService;
        private Mock<IValidator<RegisterUserDto>> _validator;
        private LoginDto _data;

        public ValidateuserFromtokenTest()
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
        public void ValidateSuccessfull()
        {
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>()))
                .Returns(new AccountDbModel() { Password = "password", Id = 1, Email = "testemail@gmail.com" });

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.IsNotNull(userService.ValidateUserByToken(_data,"1","testemail@gmail.com"));
        }

        [Test]
        public void NotValidData()
        {
            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.Throws<ArgumentNullException>(() => userService.ValidateUserByToken(_data, string.Empty, string.Empty));
        }

        [Test]
        public void NotFoundUser()
        {
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>()))
                .Returns(new AccountDbModel() { Password = "pass" });

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.Throws<NotFoundUserException>(() => userService.ValidateUserByToken(_data, "1", "testemail@gmail.com"));
        }

        [Test]
        public void NotAuthorizedUser()
        {
            _factoryRepository.Setup(x => x.Accounts.GetByLogin(It.IsAny<string>()))
                .Returns(new AccountDbModel() { Password = "password", Id = 2, Email = "test@gmail.com"});

            var userService = new AccountService(_factoryRepository.Object, _emailService.Object, _validator.Object);
            Assert.Throws<AuthorizedDataException>(() => userService.ValidateUserByToken(_data, "1", "testemail@gmail.com"));
        }
    }
}
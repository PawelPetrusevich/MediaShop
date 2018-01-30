using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;


namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    using FluentValidation;

    using Services;
    using Common.Interfaces.Services;

    [TestFixture]
    public class RegistrationServiceTest
    {
        private Mock<IAccountRepository> _store;
        private Mock<IProfileRepository> _storeProfile;
        private Mock<ISettingsRepository> _storeSettings;
        private Mock<IStatisticRepository> _storeStatistic;
        private RegisterUserDto _user;
        private Mock<IEmailService> _emailService;
        private Mock<IValidator<RegisterUserDto>> _validator;

        public RegistrationServiceTest()
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
            var mockRepository =  new Mock<IAccountRepository>();
            var mockRepositoryProfile = new Mock<IProfileRepository>();
            var mockRepositorySettings = new Mock<ISettingsRepository>();
            var mockStatisticRepository = new Mock<IStatisticRepository>();
            var mockEmailService = new Mock<IEmailService>();
            var mockValidator = new Mock<IValidator<RegisterUserDto>>();

            _store = mockRepository;
            _storeProfile = mockRepositoryProfile;
            _storeSettings = mockRepositorySettings;
            _storeStatistic = mockStatisticRepository;
            _emailService = mockEmailService;
            _validator = mockValidator;

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
            /*var profile = new ProfileDbModel { Id = 1 };
            var account = new AccountDbModel
            {
                Id = 2,
                Login = "Ivan",
                Password = "111",
                Profile = profile,
                Permissions = new List<PermissionDbModel>() { new PermissionDbModel() }
            };

            _store.Setup(x => x.Add(It.IsAny<AccountDbModel>())).Returns(account);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<AccountDbModel, bool>>>()))
                .Returns((IEnumerable<AccountDbModel>) null);
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<string>(), It.IsAny<long>())).Returns(true);

            var userService = new AccountService(_store.Object, _storeProfile.Object, _storeSettings.Object,
                 _storeStatistic.Object, _emailService.Object, this._validator.Object);

            Assert.IsNotNull(userService.Register(_user));*/
        }

        [Test]
        public void TestExistingLogin()
        {
            _store.Setup(x => x.Add(It.IsAny<AccountDbModel>())).Returns(new AccountDbModel());
            _store.Setup(x => x.GetByLogin(It.IsAny<string>())).Returns(new AccountDbModel());
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(false);

            var userService = new AccountService(_store.Object, _storeProfile.Object, _storeSettings.Object,
                _storeStatistic.Object, _emailService.Object, this._validator.Object);
           
            Assert.Throws<ExistingLoginException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFailInRepository()
        {
            _store.Setup(x => x.Add(It.IsAny<AccountDbModel>())).Returns((AccountDbModel)null);            
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<string>(), It.IsAny<long>())).Returns(true);

            var userService = new AccountService(_store.Object, _storeProfile.Object, _storeSettings.Object,
                 _storeStatistic.Object, _emailService.Object, this._validator.Object);
            
            Assert.Throws<AddAccountException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFailSendConfirmation()
        {
            _store.Setup(x => x.Add(It.IsAny<AccountDbModel>())).Returns(new AccountDbModel());
            _validator.Setup(v => v.Validate(new RegisterUserDto()).IsValid).Returns(true);
            _emailService.Setup(x => x.SendConfirmation(It.IsAny<string>(), It.IsAny<long>())).Returns(false);

            var userService = new AccountService(_store.Object, _storeProfile.Object, _storeSettings.Object,
                 _storeStatistic.Object, _emailService.Object, this._validator.Object);
            
            Assert.Throws<CanNotSendEmailException>(() => userService.Register(_user));
        }
    }
}

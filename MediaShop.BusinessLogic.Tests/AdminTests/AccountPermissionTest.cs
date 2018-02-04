using System;
using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture]
    public class AccountPermissionTest
    {
        private Mock<IAccountFactoryRepository> _factoryRepository;
        private Mock<IEmailService> _emailService;
        private Mock<IValidator<RegisterUserDto>> _validator;
        private PermissionDto _permissionCreate;
        private PermissionDto _permissionDelete;

        public AccountPermissionTest()
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

            _permissionCreate = new PermissionDto()
            {
                Id = 1,
                Permissions = Permissions.Create
            };
            _permissionDelete = new PermissionDto()
            {
                Id = 1,
                Permissions = Permissions.Delete
            };

        }

        [Test]
        public void SetPermissionSuccessfull()
        {
            var account = new AccountDbModel();

            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns(account);

            var accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);
            Assert.AreEqual(Permissions.See|Permissions.Create,accountService.SetPermission(_permissionCreate).Permissions);       

            accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);
            Assert.AreEqual(Permissions.See|Permissions.Create|Permissions.Delete,accountService.SetPermission(_permissionDelete).Permissions);
        }
        [Test]
        public void RemovePermissionSuccessfull()
        {
            var account =
                new AccountDbModel() {Permissions = Permissions.See | Permissions.Create | Permissions.Delete};
         
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns(account);
            _factoryRepository.Setup(x => x.Accounts.Update(It.IsAny<AccountDbModel>())).Returns(account);

            var accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);
            Assert.AreEqual(Permissions.See|Permissions.Create, accountService.RemovePermission(_permissionDelete).Permissions);

            accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);
            Assert.AreEqual(Permissions.See, accountService.RemovePermission(_permissionCreate).Permissions);
        }

        [Test]
        public void SetPermissionNotFoundUser()
        {
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns((AccountDbModel)null);

            var accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);

            Assert.Throws<NotFoundUserException>(() => accountService.SetPermission(_permissionCreate));
        }

        [Test]
        public void RemovePermissionNotFoundUser()
        {
            var permissionCreate = new PermissionDto()
            {
                Id = 1,
                Permissions = Permissions.Create
            };
            _factoryRepository.Setup(x => x.Accounts.Get(It.IsAny<long>())).Returns((AccountDbModel)null);

            var accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);

            Assert.Throws<NotFoundUserException>(() => accountService.RemovePermission(permissionCreate));
        }

        [Test]
        public void SetPermissionInvalidParameters()
        {
            var accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);

            Assert.Throws<ArgumentNullException>(() => accountService.SetPermission(null));
        }

        [Test]
        public void RemovePermissionInvalidParameters()
        {
            var accountService = new AccountService(_factoryRepository.Object, _emailService.Object, this._validator.Object);

            Assert.Throws<ArgumentNullException>(() => accountService.RemovePermission(null));
        }
    }
}
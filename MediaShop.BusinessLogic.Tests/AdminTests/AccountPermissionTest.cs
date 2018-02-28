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
using MediaShop.Common.Interfaces.Repositories;

namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    [TestFixture] 
    public class AccountPermissionTest
    {
        private Mock<IAccountRepository> _accountRepository;       
        private UserDto _permissionCreate;
        private UserDto _permissionDelete;

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
            var mockAccountRepository = new Mock<IAccountRepository>();

            _accountRepository = mockAccountRepository;

            _permissionCreate = new UserDto()
            {
                Id = 1,
                Permissions = Permissions.Create
            };
            _permissionDelete = new UserDto()
            {
                Id = 1,
                Permissions = Permissions.Delete
            };

        }

        [Test]
        public void SetPermissionSuccessfull()
        {
            var account = new AccountDbModel();

            _accountRepository.Setup(x => x.Get(It.IsAny<long>())).Returns(account);
            _accountRepository.Setup(x => x.Update(It.IsAny<AccountDbModel>())).Returns(account);

            var accountService = new PermissionService(_accountRepository.Object);
            Assert.AreEqual(Permissions.See|Permissions.Create,accountService.SetPermission(_permissionCreate).Permissions);       

            accountService = new PermissionService(_accountRepository.Object);
            Assert.AreEqual(Permissions.See|Permissions.Create|Permissions.Delete,accountService.SetPermission(_permissionDelete).Permissions);
        }
        [Test]
        public void RemovePermissionSuccessfull()
        {
            var account =
                new AccountDbModel() {Permissions = Permissions.See | Permissions.Create | Permissions.Delete};
         
            _accountRepository.Setup(x => x.Get(It.IsAny<long>())).Returns(account);
            _accountRepository.Setup(x => x.Update(It.IsAny<AccountDbModel>())).Returns(account);

            var accountService = new PermissionService(_accountRepository.Object);
            Assert.AreEqual(Permissions.See|Permissions.Create, accountService.RemovePermission(_permissionDelete).Permissions);

            accountService = new PermissionService(_accountRepository.Object);
            Assert.AreEqual(Permissions.See, accountService.RemovePermission(_permissionCreate).Permissions);
        }

        [Test]
        public void SetPermissionNotFoundUser()
        {
            _accountRepository.Setup(x => x.Get(It.IsAny<long>())).Returns((AccountDbModel)null);

            var accountService = new PermissionService(_accountRepository.Object);

            Assert.Throws<NotFoundUserException>(() => accountService.SetPermission(_permissionCreate));
        }

        [Test]
        public void RemovePermissionNotFoundUser()
        {
            var permissionCreate = new UserDto()
            {
                Id = 1,
                Permissions = Permissions.Create
            };
            _accountRepository.Setup(x => x.Get(It.IsAny<long>())).Returns((AccountDbModel)null);

            var accountService = new PermissionService(_accountRepository.Object);

            Assert.Throws<NotFoundUserException>(() => accountService.RemovePermission(permissionCreate));
        }

        [Test]
        public void SetPermissionInvalidParameters()
        {
            var accountService = new PermissionService(_accountRepository.Object);

            Assert.Throws<ArgumentNullException>(() => accountService.SetPermission(null));
        }

        [Test]
        public void RemovePermissionInvalidParameters()
        {
            var accountService = new PermissionService(_accountRepository.Object);

            Assert.Throws<ArgumentNullException>(() => accountService.RemovePermission(null));
        }
    }
}
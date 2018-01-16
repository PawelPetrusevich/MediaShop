using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;


namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    using MediaShop.BusinessLogic.Services;

    using Profile = MediaShop.Common.Models.User.Profile;

    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IAccountRepository> _store;
        private Mock<IPermissionRepository> _storePermission;
        private AccountDomain _user;

        public UserServiceTest()
        {
            Mapper.Initialize(x =>  x.AddProfile<MapperProfile>());
        }

        [SetUp]
        public void Init()
        {
            var mockRepository =  new Mock<IAccountRepository>();
            var mockPermitionRepository = new Mock<IPermissionRepository>();
            _store = mockRepository;
            _storePermission = mockPermitionRepository;

            _user = new AccountDomain()
            {                
                Login = "User",
                Password = "12345",  
                Email = "12345",
                Permissions = new List<PermissionDomain>(),
                Profile = new ProfileBl(),
                Settings = new SettingsDomain()
            };
        }

        [Test]
        public void TestRegistrationSuccessfull()
        {            
            var profile = new Profile { Id = 1 };
            var account = new Account
            {
                Id = 2,
                Login = "Ivan",
                Password = "111",
                Profile = profile,
                Permissions = new List<Permission>() { new Permission() }
            };

            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(account);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns((IEnumerable<Account>)null); 

            var userService = new UserService(_store.Object,_storePermission.Object);           

            Assert.IsNotNull(userService.Register(_user));
        }

        [Test]
        public void TestExistingLogin()
        {
            var permissions = new SortedSet<Role> { Role.User };
            var profile = new Profile { Id = 1 };
            var account = new Account
            {
                Login = "User",
                Password = "12345",
                Profile = profile,
                Permissions = new List<Permission>() { new Permission() }
            };
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(new Account());
            _store.Setup(x => x.GetByLogin(It.IsAny<string>())).Returns(new Account());

            var userService = new UserService(_store.Object,_storePermission.Object);
            Assert.Throws<ExistingLoginException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFailInRepository()
        {
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns((Account)null);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns((IEnumerable<Account>)null);

            var userService = new UserService(_store.Object,_storePermission.Object);

            Assert.IsNull(userService.Register(_user));
        }
    }
}

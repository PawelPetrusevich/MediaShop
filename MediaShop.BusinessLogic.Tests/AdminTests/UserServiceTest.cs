using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Dto;
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
        private UserDto _user;

        public UserServiceTest()
        {
            Mapper.Initialize(config => config.CreateMap<UserDto, Account>());
        }

        [SetUp]
        public void Init()
        {
            var mockRepository =  new Mock<IAccountRepository>();
            _store = mockRepository;

            _user = new UserDto
            {
                Id = 0,
                Login = "User",
                Password = "12345",
                UserRole = Role.User
            };
        }

        [Test]
        public void TestRegistrationSuccessfull()
        {
            var permissions = new SortedSet<Role> { Role.User };
            var profile = new Profile { Id = 1 };
            var account = new Account
            {
                Id = 2,
                Login = "Ivan",
                Password = "111",
                Profile = profile,
                Permissions = permissions
            };

            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(account);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns((IEnumerable<Account>)null); ;

            var userService = new UserService(_store.Object);           

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
                Permissions = permissions
            };
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(new Account());
            _store.Setup(x => x.GetByLogin(It.IsAny<string>())).Returns(new Account());

            var userService = new UserService(_store.Object);
            Assert.Throws<ExistingLoginException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFailInRepository()
        {
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns((Account)null);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns((IEnumerable<Account>)null);

            var userService = new UserService(_store.Object);

            Assert.IsNull(userService.Register(_user));
        }
    }
}

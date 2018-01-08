using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Dto;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Moq;
using NUnit.Framework;


namespace MediaShop.BusinessLogic.Tests.AdminTests
{
    using MediaShop.BusinessLogic.Services;

    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IRespository<Account>> _store;
        private UserDto _user;

        public UserServiceTest()
        {
            Mapper.Initialize(config => config.CreateMap<UserDto, Account>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(x => x.Login, opt => opt.MapFrom(m => m.Login))
                .ForMember(x => x.Password, opt => opt.MapFrom(m => m.Password))
                .ForMember(x => x.CreatorId, opt => opt.MapFrom(m => m.CreatorId))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.ModifierId, opt => opt.MapFrom(m => m.ModifierId))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(m => m.ModifiedDate)));
        }

        [SetUp]
        public void Init()
        {
            var mockRepository =  new Mock<IRespository<Account>>();
            _store = mockRepository;

            _user = new UserDto
            {
                Login = "User",
                Password = "12345",
                UserRole = Role.User
            };
        }

        [Test]
        public void TestRegistrationSuccessfull()
        {
            var permissions = new SortedSet<Role> { Role.User };
            var profile = new AccountProfile { Id = 1 };
            var account = new Account
            {
                Id = 2,
                Login = "User",
                Password = "12345",
                Profile = profile,
                Permissions = permissions
            };

            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(account);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>());

            var userService = new UserService(_store.Object);           

            Assert.IsTrue(userService.Register(_user));
        }

        [Test]
        public void TestExistingLogin()
        {
            var permissions = new SortedSet<Role> { Role.User };
            var profile = new AccountProfile { Id = 1 };
            var account = new Account
            {
                Login = "User",
                Password = "12345",
                Profile = profile,
                Permissions = permissions
            };
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns(new Account());
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>{account});

            var userService = new UserService(_store.Object);
            Assert.Throws<ExistingLoginException>(() => userService.Register(_user));
        }

        [Test]
        public void TestRegistraionFail()
        {
            _store.Setup(x => x.Add(It.IsAny<Account>())).Returns((Account)null);
            _store.Setup(x => x.Find(It.IsAny<Expression<Func<Account, bool>>>())).Returns(new List<Account>());

            var userService = new UserService(_store.Object);

            Assert.IsFalse(userService.Register(_user));
        }
    }
}

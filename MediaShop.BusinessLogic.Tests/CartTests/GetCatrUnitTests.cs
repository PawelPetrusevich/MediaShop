using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    /// <summary>
    /// Summary description for GetCatrUnitTests
    /// </summary>
    [TestClass]
    public class GetCatrUnitTests
    {
        private Mock<ICartRespository<ContentCartDto>> mock;

        [TestInitialize]
        public void Initialize()
        {
            // Create Mapper for testing
            AutoMapperConfiguration.Configure();

            // Create Mock
            var _mock = new Mock<ICartRespository<ContentCartDto>>();
            mock = _mock;
        }

        [TestMethod]
        public void Get_Cart()
        {
            
        }

        [TestMethod]
        public void Get_Price()
        {
            
        }

        [TestMethod]
        public void Get_Count_Items_in_Cart()
        {
          
        }     
    }
}

using System.Linq;
using MediaShop.Common.Dto.Product;

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.Content;
    using MediaShop.Common.Models.CartModels;

    public interface IProductRepository : IRepository<Product>
    {
    }
}

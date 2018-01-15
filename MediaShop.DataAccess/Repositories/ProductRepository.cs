using System.Data.Entity;
using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.CartModels;
    using MediaShop.Common.Interfaces.Repositories;

    /// <summary>
    /// Class ProductRepository
    /// </summary>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Content;
using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    public class ProductRepositoryAsync : RepositoryAsync<Product>, IProductRepositoryAsync
    {
        public ProductRepositoryAsync(DbContext context)
            : base(context)
        {
        }
    }
}

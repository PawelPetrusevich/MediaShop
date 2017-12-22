﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Content;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface IProductRepository : IRespository<Product>
    {
        IQueryable<Product> Products();
    }
}

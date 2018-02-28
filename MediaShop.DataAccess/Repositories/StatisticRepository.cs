using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;
using MediaShop.DataAccess.Properties;
using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    public class StatisticRepository : Repository<StatisticDbModel>, IStatisticRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public StatisticRepository(DbContext context)
            : base(context)
        {
        }     
    }
}
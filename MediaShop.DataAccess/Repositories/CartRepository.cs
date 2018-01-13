namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Repositories.Base;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class CartRepository : Repository<ContentCart>, ICartRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CartRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Method for getting collection objects type ContentCart
        /// by user identificator
        /// </summary>
        /// <param name="userId">identificator user</param>
        /// <returns>rezalt operation</returns>
        public IEnumerable<ContentCart> GetAll(long userId)
        {
            var result = this.DbSet.Where(x => x.CreatorId == userId);
            return Mapper.Map<IEnumerable<ContentCart>, IEnumerable<ContentCart>>(result);
        }
    }
}

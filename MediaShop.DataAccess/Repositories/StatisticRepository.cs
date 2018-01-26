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

        /// <summary>
        /// Method Get
        /// gets user by ID
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>accounty</returns>
        /// <exception cref="ArgumentException">if id = 0</exception>
        public override StatisticDbModel Get(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            return this.DbSet.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// add model to repository
        /// </summary>
        /// <param name="model">model statistic</param>
        /// <returns>statistic</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public override StatisticDbModel Add(StatisticDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = this.DbSet.Add(model);
            this.Context.SaveChanges();

            return result;
        }

        /// <summary>
        ///  method update statistic
        /// </summary>
        /// <param name="model">statistic to update</param>
        /// <returns>Updated statistic</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override StatisticDbModel Update(StatisticDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.Context.Entry(model).State = EntityState.Modified;
            this.Context.SaveChanges();

            return model;
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable statistic</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override IEnumerable<StatisticDbModel> Find(Expression<Func<StatisticDbModel, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.DbSet.AsNoTracking().Where(filter);
        }

        /// <summary>
        /// Method delete statistic by model
        /// </summary>
        /// <param name="model">statistic to delete</param>
        /// <returns>Deleted statistic</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override StatisticDbModel Delete(StatisticDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.DbSet.Contains(model))
            {
                this.Context.Entry(model).State = EntityState.Deleted;
                this.Context.SaveChanges();

                return model;
            }

            return null;
        }

        /// <summary>
        /// method delete statistic by id
        /// </summary>
        /// <param name="id">statistic  Id</param>
        /// <returns>Deleted statistic</returns>
        public override StatisticDbModel Delete(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var model = this.DbSet.SingleOrDefault(entity => entity.Id == id);

            if (model != null)
            {
                this.Context.Entry(model).State = EntityState.Deleted;
                this.Context.SaveChanges();

                return model;
            }

            return null;
        }
    }
}
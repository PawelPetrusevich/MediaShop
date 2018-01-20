// <copyright file="SettingsRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Properties;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Class SettingsRepository.
    /// </summary>
    /// <seealso cref="Repository{T}" />
    /// <seealso cref="ISettingsRepository" />
    public class SettingsRepository : Repository<SettingsDbModel>, ISettingsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SettingsRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Method Get
        /// gets settings by ID
        /// </summary>
        /// <param name="id">settings id</param>
        /// <returns>settings</returns>
        /// <exception cref="ArgumentException">if id = 0</exception>
        public override SettingsDbModel Get(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            return this.DbSet.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// add settings to repository
        /// </summary>
        /// <param name="model">model settings</param>
        /// <returns>settings</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public override SettingsDbModel Add(SettingsDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                this.Context.Configuration.AutoDetectChangesEnabled = false;

                var account = this.DbSet.Add(model);
                this.Context.SaveChanges();

                return account;
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///  method update settings
        /// </summary>
        /// <param name="model">Settings to update</param>
        /// <returns>Updated settings</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override SettingsDbModel Update(SettingsDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            using (this.Context)
            {
                this.Context.Entry(model).State = EntityState.Modified;
                this.Context.SaveChanges();

                return model;
            }
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable settings</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override IEnumerable<SettingsDbModel> Find(Expression<Func<SettingsDbModel, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.DbSet.AsNoTracking().Where(filter);
        }

        /// <summary>
        /// Method delete Settings by model
        /// </summary>
        /// <param name="model">Account to delete</param>
        /// <returns>Deleted settings</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override SettingsDbModel Delete(SettingsDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.DbSet.Contains(model))
            {
                using (this.Context)
                {
                    this.Context.Entry(model).State = EntityState.Deleted;
                    this.Context.SaveChanges();

                    return model;
                }
            }

            return null;
        }

        /// <summary>
        /// method delete settings by id
        /// </summary>
        /// <param name="id">settings  Id</param>
        /// <returns>Deleted settings</returns>
        public override SettingsDbModel Delete(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var model = this.DbSet.AsNoTracking().SingleOrDefault(entity => entity.Id == id);

            if (model != null)
            {
                using (this.Context)
                {
                    this.Context.Entry(model).State = EntityState.Deleted;
                    this.Context.SaveChanges();

                    return model;
                }
            }

            return null;
        }
    }
}
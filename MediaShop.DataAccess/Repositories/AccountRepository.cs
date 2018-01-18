// <copyright file="AccountRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Properties;

    /// <summary>
    /// Class AccountRepository.
    /// </summary>
    /// <seealso cref="Repository{T}" />
    /// <seealso cref="IAccountRepository" />
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AccountRepository(DbContext context)
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
        public override Account Get(long id)
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
        /// <param name="model">model account</param>
        /// <returns>account</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public override Account Add(Account model)
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
        ///  method update account
        /// </summary>
        /// <param name="model">Account to update</param>
        /// <returns>Updated account</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override Account Update(Account model)
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
        /// <returns>Suitable accounts</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override IEnumerable<Account> Find(Expression<Func<Account, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.DbSet.AsNoTracking().Where(filter);
        }

        /// <summary>
        /// Method delete Account by model
        /// </summary>
        /// <param name="model">Account to delete</param>
        /// <returns>Deleted account</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override Account Delete(Account model)
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
        /// method delete account by id
        /// </summary>
        /// <param name="id">account  Id</param>
        /// <returns>Deleted account</returns>
        public override Account Delete(long id)
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

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Account</returns>
        /// <exception cref="ArgumentException">if login is null or empty string</exception>
        public Account GetByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(Resources.InvalidLoginValue);
            }

            return this.DbSet.AsNoTracking().SingleOrDefault(account => account.Login.Equals(login));
        }
    }
}
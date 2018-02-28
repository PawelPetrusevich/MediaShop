// <copyright file="AccountRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>
namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Properties;
    using MediaShop.DataAccess.Repositories.Base;

    /// <summary>
    /// Class AccountRepository.
    /// </summary>
    /// <seealso cref="Repository{T}" />
    /// <seealso cref="IAccountRepository" />
    public class AccountRepository : Repository<AccountDbModel>, IAccountRepository
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
        public override AccountDbModel Get(long id)
        {
            if (id == 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            return this.DbSet.Include(m => m.Profile).Include(m => m.Settings).SingleOrDefault(x => x.Id == id);
        }

        public async Task<AccountDbModel> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            return await DbSet.Include(m => m.Profile).Include(m => m.Settings).SingleOrDefaultAsync(entity => entity.Id == id);
        }

        /// <summary>
        /// add model to repository
        /// </summary>
        /// <param name="model">model account</param>
        /// <returns>account</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public override AccountDbModel Add(AccountDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var account = this.DbSet.Add(model);
            this.Context.SaveChanges();

            return account;
        }

        /// <summary>
        ///  method update account
        /// </summary>
        /// <param name="model">Account to update</param>
        /// <returns>Updated account</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override AccountDbModel Update(AccountDbModel model)
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
        /// Get user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<AccountDbModel> GetByLoginAsync(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(Resources.InvalidLoginValue);
            }

            return await this.DbSet.Include(m => m.Profile).Include(m => m.Settings).SingleOrDefaultAsync(account => account.Login.Equals(login));
        }

        public async Task<AccountDbModel> UpdateAsync(AccountDbModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.Context.Entry(model).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();

            return model;
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable accounts</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override IEnumerable<AccountDbModel> Find(Expression<Func<AccountDbModel, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.DbSet.Include(m => m.Profile).Include(m => m.Settings).Where(filter);
        }

        /// <summary>
        /// Method delete Account by model
        /// </summary>
        /// <param name="model">Account to delete</param>
        /// <returns>Deleted account</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public override AccountDbModel Delete(AccountDbModel model)
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
        /// method delete account by id
        /// </summary>
        /// <param name="id">account  Id</param>
        /// <returns>Deleted account</returns>
        public override AccountDbModel Delete(long id)
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

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Account</returns>
        /// <exception cref="ArgumentException">if login is null or empty string</exception>
        public AccountDbModel GetByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(Resources.InvalidLoginValue);
            }

            return this.DbSet.Include(m => m.Profile).Include(m => m.Settings).SingleOrDefault(account => account.Login.Equals(login));
        }

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Account</returns>
        /// <exception cref="ArgumentException">if login is null or empty string</exception>
        public AccountDbModel GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(Resources.InvalidLoginValue);
            }

            return this.DbSet.Include(m => m.Profile).Include(m => m.Settings).SingleOrDefault(account => account.Email.Equals(email));
        }

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Account</returns>
        /// <exception cref="ArgumentException">if login is null or empty string</exception>
        public Task<AccountDbModel> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(Resources.InvalidLoginValue);
            }

            return this.DbSet.Include(m => m.Profile).Include(m => m.Settings).SingleOrDefaultAsync(account => account.Email.Equals(email));
        }

        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>IEnumerable<AccountDbModel</returns>
        public IEnumerable<AccountDbModel> GetAllUsers()
        {
            return this.DbSet.ToList();
        }

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        /// <param name="id">account  Id</param>
        /// <returns>Account</returns>
        public AccountDbModel SoftDelete(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var model = this.DbSet.SingleOrDefault(entity => entity.Id == id);
            model.IsDeleted = true;
            this.Context.Entry(model).State = EntityState.Modified;
            this.Context.SaveChanges();
            return model;
        }

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        /// <param name="id">account  Id</param>
        /// <returns>Account</returns>
        public async Task<AccountDbModel> SoftDeleteAsync(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var model = await this.DbSet.SingleOrDefaultAsync(entity => entity.Id == id);
            model.IsDeleted = true;
            this.Context.Entry(model).State = EntityState.Modified;
            this.Context.SaveChangesAsync().ConfigureAwait(false);

            return model;
        }
    }
}

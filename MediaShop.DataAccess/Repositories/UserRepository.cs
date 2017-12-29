// <copyright file="UserRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;

    /// <summary>
    /// Class UserRepository.
    /// </summary>
    /// <typeparam name="T">Actual Entity type</typeparam>
    /// <seealso cref="MediaShop.Common.Interfaces.Repositories.IRespository{T}" />
    public class UserRepository<T> : IRespository<T>
        where T : Entity
    {
        private readonly DbContext сontext;
        private readonly DbSet<T> set;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRepository(DbContext context)
        {
            this.сontext = context;
            this.set = this.сontext.Set<T>();
        }

        /// <summary>
        /// Method Get
        /// gets user by ID
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>db entry</returns>
        public T Get(int id)
        {
            return this.set.FirstOrDefault(account => account.Id == id) ??
                   throw new ArgumentOutOfRangeException($"Invalid {nameof(id)}");
        }

        /// <summary>
        /// add model to repository
        /// </summary>
        /// <param name="model">model user</param>
        /// <returns>db entry</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public T Add(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.set.Add(model);

            return model;
        }

        /// <summary>
        /// Method Update
        /// update user data
        /// </summary>
        /// <param name="model">model_user</param>
        /// <returns>db_entry</returns>
        public T Update(T model)
        {
            T account = this.Get(model.Id);

            foreach (var property in typeof(T).GetProperties())
            {
                property.SetValue(account, property.GetValue(model));
            }

            return account;
        }

        /// <summary>
        /// Method delete
        /// delete user data by model
        /// </summary>
        /// <param name="model">model user</param>
        /// <returns>null</returns>
        public T Delete(T model)
        {
            if (this.set.Contains(model))
            {
                this.set.Remove(model);
            }

            return null;

            // что возвращать?
            // что делать, если такого профили в репозитории нет?
        }

        /// <summary>
        /// Method delete
        /// delete user data by Id
        /// </summary>
        /// <param name="id">user_id</param>
        /// <returns>null</returns>
        public T Delete(int id)
        {
            var model = this.set.FirstOrDefault(account => account.Id == id);
            if (model != null)
            {
                this.set.Remove(model);
            }

            return null;
        }

        /// <summary>
        /// Method Find
        /// find user data by expression
        /// </summary>
        /// <param name="filter">lambda expression</param>
        /// <returns>db entry</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            Func<T, bool> criteria = filter.Compile();

            return this.set.Where(criteria);
        }
    }
}

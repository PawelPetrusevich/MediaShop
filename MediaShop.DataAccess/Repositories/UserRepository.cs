using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<Account> list = new List<Account>();

        public Account Get(int id)
        {
            return this.list.FirstOrDefault(account => account.Id == id) ??
                   throw new ArgumentOutOfRangeException($"Invalid {nameof(id)}");
        }

        public Account Add(Account model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.list.Add(model);

            // взять id откуда-нибудь. Мб Random
            return model;
        }

        public Account Update(Account model)
        {
            Account account = this.Get(model.Id);

            foreach (var property in typeof(Account).GetProperties())
            {
                property.SetValue(account, property.GetValue(model));
            }

            return account;
        }

        public Account Delete(Account model)
        {
            if (this.list.Contains(model))
            {
                this.list.Remove(model);
            }

            return null;

            // что возвращать?
            // что делать, если такого профили в репозитории нет?
        }

        public Account Delete(int id)
        {
            var model = this.list.FirstOrDefault(account => account.Id == id);
            if (model != null)
            {
                this.list.Remove(model);
            }

            return null;
        }

        public IEnumerable<Account> Find(Expression<Func<Account, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            Func<Account, bool> criteria = filter.Compile();

            return this.list.Where(criteria);
        }
    }
}

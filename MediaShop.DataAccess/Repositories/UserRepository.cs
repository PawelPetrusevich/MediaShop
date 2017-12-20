namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Interfaces.Repositories;
    using Common.Models.User;

    public class UserRepository : IRespository<Profile>
    {
        private readonly List<Profile> list = new List<Profile>();

        public Profile Get(int id)
        {
            return this.list.FirstOrDefault(profile => profile.Id == id) ??
                   throw new ArgumentOutOfRangeException($"Invalid {nameof(id)}");
        }

        public Profile Add(Profile model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.list.Add(model);

            // взять id откуда-нибудь. Мб Random, хрен пойми
            return model;
        }

        public Profile Update(Profile model)
        {
            Profile profile = this.Get(model.Id);

            foreach (var property in typeof(Profile).GetProperties())
            {
                property.SetValue(profile, property.GetValue(model));
            }

            return profile;
        }

        public Profile Delete(Profile model)
        {
            if (this.list.Contains(model))
            {
                this.list.Remove(model);
            }

            return null;

            // что возвращать?
            // что делать, если такого профили в репозитории нет?
        }

        public Profile Delete(int id)
        {
            var model = this.list.FirstOrDefault(profile => profile.Id == id);
            if (model != null)
            {
                this.list.Remove(model);
            }

            return null;
        }

        public IEnumerable<Profile> Find(Expression<Func<Profile, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            Func<Profile, bool> criteria = filter.Compile();

            return this.list.Where(criteria);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Models;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface IRespository<TModel> where TModel : Entity
    {
        TModel Get(int id);

        TModel Add(TModel model);

        TModel Update(TModel model);

        TModel Delete(TModel model);

        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}
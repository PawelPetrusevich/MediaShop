namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.CartModels;

    public interface IProductRepository<TModel>
        where TModel : Product
    {
        TModel Get(ulong id);

        TModel Add(TModel model);

        TModel Update(TModel model);

        TModel Delete(TModel model);

        TModel Delete(ulong id);

        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Dto;

    public interface IProductRepository<TModel> : IDisposable
        where TModel : ProductDto
    {
        TModel Get(ulong id);

        TModel Add(TModel model);

        TModel Update(TModel model);

        TModel Delete(TModel model);

        TModel Delete(ulong id);

        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> filter);
    }
}

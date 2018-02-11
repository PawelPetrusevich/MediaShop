using System.Linq;
using System.Threading.Tasks;
using MediaShop.Common.Dto.Product;

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.Content;

    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Soft delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Product SoftDelete(long id);

        /// <summary>
        /// Soft delete async
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Task Product</returns>
        Task<Product> SoftDeleteAsync(long id);

        /// <summary>
        /// Get list products on sale
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IEnumerable<Product> GetListOnSale();

        Task<IEnumerable<Product>> GetListOnSaleAsync();

        Task<Product> AddAsync(Product model);

        Task<Product> DeleteAsync(Product model);

        Task<Product> DeleteAsync(long id);

        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> filter);

        Task<Product> GetAsync(long id);

        Task<Product> UpdateAsync(Product model);
    }
}

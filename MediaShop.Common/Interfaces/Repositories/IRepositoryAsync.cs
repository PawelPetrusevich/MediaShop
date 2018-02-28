using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface IRepositoryAsync<TModel> : IDisposable
    {
        /// <summary>
        /// Interface method get for type TModel
        /// </summary>
        /// <param name="id">Id of searching element</param>
        /// <returns>TModel</returns>
        Task<TModel> GetAsync(long id);

        /// <summary>
        /// interface method add for type TModel
        /// </summary>
        /// <param name="model">Model to add</param>
        /// <returns>Added Model</returns>
        Task<TModel> AddAsync(TModel model);

        /// <summary>
        ///  interface method update for type TModel
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>Updated model</returns>
        Task<TModel> UpdateAsync(TModel model);

        /// <summary>
        /// interface method delete for type TModel by model
        /// </summary>
        /// <param name="model">Model to delete</param>
        /// <returns>Instance of deleted model</returns>
        Task<TModel> DeleteAsync(TModel model);

        /// <summary>
        /// interface method delete for type TModel by id
        /// </summary>
        /// <param name="id">Id of deleting model</param>
        /// <returns>Deleted model</returns>
        Task<TModel> DeleteAsync(long id);

        /// <summary>
        /// Find by id
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable entities</returns>
        Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> filter);
    }
}
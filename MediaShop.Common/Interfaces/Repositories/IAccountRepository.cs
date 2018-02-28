// <copyright file="UserRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IAccountRepository
    /// <seealso cref="IRepository{Account}" />
    public interface IAccountRepository : IRepository<AccountDbModel>, IRepositoryAsync<AccountDbModel>
    {
        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Entity</returns>
        AccountDbModel GetByLogin(string login);

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Entity</returns>
        Task<AccountDbModel> GetByLoginAsync(string login);

        /*Task<AccountDbModel> UpdateAsync(AccountDbModel user);
        Task<AccountDbModel> GetAsync(long id);*/

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="email">The Email.</param>
        /// <returns>Entity</returns>
        AccountDbModel GetByEmail(string email);

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="email">The Email.</param>
        /// <returns>Entity</returns>
        Task<AccountDbModel> GetByEmailAsync(string email);

        IEnumerable<AccountDbModel> GetAllUsers();

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        /// <param name="id">user</param>
        /// <returns>Entity</returns>
        AccountDbModel SoftDelete(long id);

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        /// <param name="id">user</param>
        /// <returns>Entity</returns>
        Task<AccountDbModel> SoftDeleteAsync(long id);
    }
}
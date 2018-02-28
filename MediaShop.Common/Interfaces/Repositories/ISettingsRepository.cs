// <copyright file="ISettingsRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface ISettingsRepository
    /// </summary>
    /// <seealso cref="MediaShop.Common.Interfaces.Repositories.IRepository{Settings}" />
    public interface ISettingsRepository : IRepository<SettingsDbModel>, IRepositoryAsync<SettingsDbModel>
    {
    }
}
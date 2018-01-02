// <copyright file="IAccountSettingsRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IAccountSettingsRepository
    /// </summary>
    /// <seealso cref="MediaShop.Common.Interfaces.Repositories.IRepository{AccountSettings}" />
    public interface IAccountSettingsRepository : IRepository<AccountSettings>
    {
    }
}
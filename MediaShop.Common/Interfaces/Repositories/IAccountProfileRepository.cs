// <copyright file="IAccountProfileRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IAccountProfile
    /// </summary>
    /// <seealso cref="Common.Interfaces.Repositories.IRepository{AccountProfile}" />
    public interface IAccountProfileRepository : IRepository<AccountProfile>
    {
    }
}
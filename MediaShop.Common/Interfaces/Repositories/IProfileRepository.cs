﻿// <copyright file="IProfileRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IProfileRepository
    /// </summary>
    /// <seealso cref="IRepository{Profile}" />
    public interface IProfileRepository : IRepository<Profile>
    {
    }
}
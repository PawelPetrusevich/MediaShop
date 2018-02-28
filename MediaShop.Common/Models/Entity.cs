// <copyright file="Entity.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models
{
    using System;

    /// <summary>
    /// Class Entity.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the creator identifier.
        /// </summary>
        /// <value>The creator identifier.</value>
        public long CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modifier identifier.
        /// </summary>
        /// <value>The modifier identifier.</value>
        public long? ModifierId { get; set; }
    }
}
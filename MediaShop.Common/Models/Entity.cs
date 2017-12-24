namespace MediaShop.Common.Models
{
    using System;

    /// <summary>
    /// Сlass describes entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets object identificator
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Gets or sets date of object creation
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets cart creator Id
        /// </summary>
        public ulong CreatorId { get; set; }

        /// <summary>
        /// Gets or sets date of object modification
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets object modificator
        /// </summary>
        public ulong? ModifierId { get; set; }
    }
}
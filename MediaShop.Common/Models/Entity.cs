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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets date of object creation
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets cart creator Id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Gets or sets date of object modification
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets object modificator
        /// </summary>
        public int? ModifierId { get; set; }
    }
}
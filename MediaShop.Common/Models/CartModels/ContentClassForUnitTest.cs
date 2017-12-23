namespace MediaShop.Common.Models
{
    using System;

    /// <summary>
    /// Class instead of class content for testing CartService
    /// </summary>
    public class ContentClassForUnitTest : Entity
    {
        /// <summary>
        /// Gets or sets object identificator
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets date of object creation
        /// </summary>
        public new DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets cart creator Id
        /// </summary>
        public new int CreatorId { get; set; }

        /// <summary>
        /// Gets or sets date of object modification
        /// </summary>
        public new DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets object modificator
        /// </summary>
        public new int? ModifierId { get; set; }
    }
}

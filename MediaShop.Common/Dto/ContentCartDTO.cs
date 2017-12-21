namespace MediaShop.Common.Models
{
    using System;

    /// <summary>
    /// Сlass describes model ContentCartDTO
    /// </summary>
    public class ContentCartDto : Entity
    {
        /// <summary>
        /// Gets or sets identificator content in the shopping cart
        /// </summary>
        public new int Id { get; set; }

        /// <summary>
        /// Gets or sets the content ID in the shopping cart
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the content name in the shopping cart
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets date of creation of a shopping cart
        /// </summary>
        public new DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets cart creator Id
        /// </summary>
        public new int CreatorId { get; set; }

        /// <summary>
        /// Gets or sets cart creator name
        /// </summary>
        public string NameCreator { get; set; }

        /// <summary>
        /// Gets or sets date of modification of a shopping cart
        /// </summary>
        public new DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets cart modificator Id
        /// </summary>
        public new int? ModifierId { get; set; }

        /// <summary>
        /// Gets or sets description media content
        /// </summary>
        public string DescriptionItem { get; set; }

        /// <summary>
        /// Gets or sets price media content
        /// </summary>
        public decimal PriceItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets 
        /// the property to determine whether the content is selected
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets
        /// the property to determine whether the content is bought
        /// </summary>
        public bool IsBought { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets
        /// the property to determine whether the content is delete
        /// </summary>
        public bool IsDelete { get; set; }
    }
}

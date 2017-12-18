using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Models
{
    /// <summary>
    /// Сlass describes ShoppingCart
    /// </summary>
    public class ShoppingCart
    {
        private string subjectCollectionId;

        private List<SubjectCollection> subjectCollection;

        private decimal priceAllItemsCollection;

        private int countItemsInCollection;

        public string SubjectCollectionId { get; set; }

        /// <summary>
        /// Collection items
        /// </summary>
        public List<SubjectCollection> SubjectCollection { get; set; }

        /// <summary>
        /// Property to determine price all items in collection
        /// </summary>
        public decimal PriceAllItemsCollection { get; set; }

        /// <summary>
        /// Property to determine the amount of content in a collection
        /// </summary>
        public int CountItemsInCollection { get; set; }

        /// <summary>
        /// The property to determine whether the collection is selected
        /// </summary>
        public bool IsChecked { get; set; }
    }
}

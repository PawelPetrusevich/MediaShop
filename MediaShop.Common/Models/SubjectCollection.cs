using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Models
{
    /// <summary>
    /// Сlass describes Collection items
    /// </summary>
    public class SubjectCollection
    {
        private int contentId;

        private string descriptionItem;

        private decimal priceItem;

        /// <summary>
        /// Id content
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        /// Description media content
        /// </summary>
        public string DescriptionItem { get; set; }

        /// <summary>
        /// Price media content
        /// </summary>
        public decimal PriceItem { get; set; }

        /// <summary>
        /// The property to determine whether the content is selected
        /// </summary>
        public bool IsChecked { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Enums;

namespace MediaShop.Common.Dto.Product
{
    public class ProductInfoDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProductPrice.
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the protected content
        /// </summary>
        public string Content { get; set; }
    }
}

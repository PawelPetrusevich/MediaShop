using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Content;

namespace MediaShop.Common.Dto.Product
{
    /// <summary>
    /// DTO for download service
    /// </summary>
    public class DownloadProductDto
    {
        /// <summary>
        /// Gets or sets product id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets Product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets original product byte array
        /// </summary>
        public OriginalProduct OriginalProduct { get; set; }
    }
}

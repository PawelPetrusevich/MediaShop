using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Dto
{
    /// <summary>
    /// Dto model for notification about add to cart
    /// </summary>
    public class AddToCartNotifyDto
    {
        /// <summary>
        /// Notification receiver id
        /// </summary>
        public long ReceiverId { get; set; }

        /// <summary>
        /// Name of product
        /// </summary>
        public string ProductName { get; set; }
    }
}
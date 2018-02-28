using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Dto.Messaging
{
    public class NotificationDto
    {
        /// <summary>
        /// Gets or sets notification message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets notification title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets identifier of user who receive notification
        /// </summary>
        public long ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets identifier of user who sent notification
        /// </summary>
        public long SenderId { get; set; }
    }
}

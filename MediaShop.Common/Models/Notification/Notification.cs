using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models;

namespace MediaShop.Common.Models.Notification
{
    /// <summary>
    /// Notification Model
    /// </summary>
    public class Notification : Entity
    {
        /// <summary>
        /// Gets or sets notification message
        /// </summary>
        public string NotificationMessage { get; set; }

        // public NotificationTypes notificationTypes { get; set; }
    }
}

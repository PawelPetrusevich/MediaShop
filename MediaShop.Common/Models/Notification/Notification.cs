using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

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
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets notification title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets identifier of user who sent notification
        /// </summary>
        public long ReceiverId { get; set; }
        
        public virtual AccountDbModel Receiver { get; set; }
    }
}
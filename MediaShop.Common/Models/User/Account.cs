using System;
using System.Collections.Generic;

namespace MediaShop.Common.Models.User
{
    public class Account : Entity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatorId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifierId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Describing personal  users data
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Permissions  describes  list of roles, that has this user
        /// </summary>
        public SortedSet<Role> Permissions { get; set; }
    }
}
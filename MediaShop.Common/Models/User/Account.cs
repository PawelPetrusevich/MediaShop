using System.Collections.Generic;

namespace MediaShop.Common.Models.User
{
    public class Account : Entity
    {
        /// <summary>
        /// user login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// id from table profile
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// id from table settings
        /// </summary>
        public int SettingsId { get; set; }

        /// <summary>
        /// Describing personal  users data
        /// </summary>
        public virtual AccountProfile Profile { get; set; } = new AccountProfile();

        /// <summary>
        /// Personal user settings
        /// </summary>
        public virtual AccountSettings Settings { get; set; } = new AccountSettings();

        /// <summary>
        /// Permissions  describes  list of roles, that has this user
        /// </summary>
        public virtual ICollection<Role> Permissions { get; set; } = new SortedSet<Role>();
    }
}
using System.Collections.Generic;

namespace MediaShop.Common.Models.User
{
    public class Account : Entity
    {
        public Account()
        {
            this.Profile = new AccountProfile();
            this.Permissions = new SortedSet<Role>();
            this.Settings = new AccountSettings();
        }

        public string Login { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Describing personal  users data
        /// </summary>
        public virtual AccountProfile Profile { get; set; }

        /// <summary>
        /// Personal user settings
        /// </summary>
        public virtual AccountSettings Settings { get; set; }

        /// <summary>
        /// Permissions  describes  list of roles, that has this user
        /// </summary>
        public virtual ICollection<Role> Permissions { get; set; }
    }
}
using System.Collections.Generic;

namespace MediaShop.Common.Models.User
{
    public class Account : Entity
    {
        public Account()
        {
            this.Profile = new AccountProfile();
            this.Permissions = new SortedSet<Role>();
        }

        public string Login { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Describing personal  users data
        /// </summary>
        public AccountProfile Profile { get; set; }

        /// <summary>
        /// Permissions  describes  list of roles, that has this user
        /// </summary>
        public ICollection<Role> Permissions { get; set; }
    }
}
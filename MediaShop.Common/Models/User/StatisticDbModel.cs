using System;

namespace MediaShop.Common.Models.User
{
    /// <summary>
    /// Class StatisticDbModel.
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    public class StatisticDbModel : Entity
    {
        /// <summary>
        /// Date user Login
        /// </summary>
        public DateTime DateLogIn { get; set; } = DateTime.Now;

        /// <summary>
        /// Date user LogOut
        /// </summary>
        public DateTime? DateLogOut { get; set; }

        public long AccountId { get; set; }

        /// <summary>
        /// Gets or sets Account.
        /// </summary>
        /// <value>The account.</value>
        public virtual AccountDbModel AccountDbModel { get; set; }
    }
}
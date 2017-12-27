using System;

namespace MediaShop.Common.Models.User
{
    public class AccountProfile : Entity
    {
        /// <summary>
        /// User Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// reference to account table
        /// </summary>
        public Account AccountOf { get; set; }

        /// <summary>
        /// id from table Account
        /// </summary>
        public int AccountId { get; set; }
    }
}
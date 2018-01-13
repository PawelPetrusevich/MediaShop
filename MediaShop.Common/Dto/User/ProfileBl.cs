namespace MediaShop.Common.Dto.User
{
    using System;

    using MediaShop.Common.Models;

    /// <summary>
    /// Class ProfileBL.
    /// </summary>
    /// <seealso cref="MediaShop.Common.Models.Entity" />
    public class ProfileBl
    {
        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>The login.</value>
        public string Login { get; set; }
    }
}
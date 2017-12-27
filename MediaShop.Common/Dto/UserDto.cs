using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto
{
    /// <summary>
    /// Data transfer object for User
    /// </summary>
    public class UserDto : Entity
    {
        /// <summary>
        /// Login user
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Passwor user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Role sets permission for user
        /// </summary>
        public Role UserRole { get; set; }
    }
}
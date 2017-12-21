using System;
using MediaShop.Common.Models;

namespace MediaShop.Common.Dto
{
    public class DtoUserModel : Entity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}
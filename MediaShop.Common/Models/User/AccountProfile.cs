using System;

namespace MediaShop.Common.Models.User
{
    public class AccountProfile : Entity
    {
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public Account AccountOf { get; set; }
    }
}
namespace MediaShop.Common.Models.User
{
    public class Permission : Entity
    {
        public Role Role { get; set; } = Role.User;

        public virtual Account Account { get; set; }
    }
}
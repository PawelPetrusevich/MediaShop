namespace MediaShop.Common.Dto.User
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
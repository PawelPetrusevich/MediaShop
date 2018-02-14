using System;

namespace MediaShop.Common.Helpers
{
    public static class TokenHelper
    {
        public static string NewToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace('/', '=');
        }
    }
}
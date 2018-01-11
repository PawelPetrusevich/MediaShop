using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when user registres with existing login
    /// </summary>
    [Serializable]
    public class ExistingLoginException : Exception
    {
        public ExistingLoginException()
        {
        }

        public ExistingLoginException(string login) : base("Login already exists")
        {
            this.Login = login;
        }

        public ExistingLoginException(string message, string login, Exception innerException) : base(message, innerException)
        {
            this.Login = login;
        }

        public ExistingLoginException(string message, string login) : base(message)
        {
            this.Login = login;
        }

        protected ExistingLoginException(string login, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Login = login;
        }

        public string Login { get; private set; }
    }
}
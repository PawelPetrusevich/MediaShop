using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when can not send Email
    /// </summary>
    [Serializable]
    public class AddAccountRepositoryException : Exception
    {
        public AddAccountRepositoryException() 
        {
        }

        public AddAccountRepositoryException(string message) : base(message)
        {
        }

        public AddAccountRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddAccountRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
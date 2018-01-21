namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException()
            : base()
        {
        }

        public InvalidTransactionException(string message)
            : base(message)
        {
        }

        public InvalidTransactionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InvalidTransactionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

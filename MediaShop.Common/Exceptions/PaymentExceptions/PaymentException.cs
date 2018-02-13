namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class PaymentException : Exception
    {
        public PaymentException()
            : base()
        {
        }

        public PaymentException(string message)
            : base(message)
        {
        }

        public PaymentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PaymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

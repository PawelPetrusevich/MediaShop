namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class AddPaymentException : Exception
    {
        public AddPaymentException()
            : base()
        {
        }

        public AddPaymentException(string message)
            : base(message)
        {
        }

        public AddPaymentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AddPaymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

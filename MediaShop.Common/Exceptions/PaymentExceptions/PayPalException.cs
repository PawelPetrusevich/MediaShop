namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class PayPalException : Exception
    {
        public PayPalException()
            : base()
        {
        }

        public PayPalException(string message)
            : base(message)
        {
        }

        public PayPalException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PayPalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

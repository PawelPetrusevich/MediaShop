namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class PaymentsException : Exception
    {
        public PaymentsException()
            : base()
        {
        }

        public PaymentsException(string message)
            : base(message)
        {
        }

        public PaymentsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PaymentsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

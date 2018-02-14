namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class OperationPaymentException : Exception
    {
        public OperationPaymentException()
            : base()
        {
        }

        public OperationPaymentException(string message)
            : base(message)
        {
        }

        public OperationPaymentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public OperationPaymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

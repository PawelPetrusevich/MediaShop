namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class ExistPaymentException
    /// </summary>
    public class ExistPaymentException : Exception
    {
        public ExistPaymentException()
            : base()
        {
        }

        public ExistPaymentException(string message)
            : base(message)
        {
        }

        public ExistPaymentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ExistPaymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

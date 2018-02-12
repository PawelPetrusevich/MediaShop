namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class AddDefrayalException
    /// </summary>
    public class AddDefrayalException : Exception
    {
        public AddDefrayalException()
            : base()
        {
        }

        public AddDefrayalException(string message)
            : base(message)
        {
        }

        public AddDefrayalException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AddDefrayalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

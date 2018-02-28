namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class DeleteDefrayalException
    /// </summary>
    public class DeleteDefrayalException : Exception
    {
        public DeleteDefrayalException()
            : base()
        {
        }

        public DeleteDefrayalException(string message)
            : base(message)
        {
        }

        public DeleteDefrayalException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public DeleteDefrayalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

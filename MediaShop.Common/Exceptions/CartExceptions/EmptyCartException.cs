namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class EmptyCartException : Exception
    {
        public EmptyCartException()
            : base()
        {
        }

        public EmptyCartException(string message)
            : base(message)
        {
        }

        public EmptyCartException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EmptyCartException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

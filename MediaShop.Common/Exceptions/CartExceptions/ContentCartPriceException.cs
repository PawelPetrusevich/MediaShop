namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class ContentCartPriceException : Exception
    {
        public ContentCartPriceException()
            : base()
        {
        }

        public ContentCartPriceException(string message)
            : base(message)
        {
        }

        public ContentCartPriceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ContentCartPriceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

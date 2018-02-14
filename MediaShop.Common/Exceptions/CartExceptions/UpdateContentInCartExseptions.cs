namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class UpdateContentInCartExseptions : Exception
    {
        public UpdateContentInCartExseptions()
            : base()
        {
        }

        public UpdateContentInCartExseptions(string message)
            : base(message)
        {
        }

        public UpdateContentInCartExseptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public UpdateContentInCartExseptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

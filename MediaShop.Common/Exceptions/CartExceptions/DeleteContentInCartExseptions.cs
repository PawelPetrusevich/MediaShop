namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class DeleteContentInCartExseptions : Exception
    {
        public DeleteContentInCartExseptions()
            : base()
        {
        }

        public DeleteContentInCartExseptions(string message)
            : base(message)
        {
        }

        public DeleteContentInCartExseptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public DeleteContentInCartExseptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class DeleteContentInCartExceptions : Exception
    {
        public DeleteContentInCartExceptions()
            : base()
        {
        }

        public DeleteContentInCartExceptions(string message)
            : base(message)
        {
        }

        public DeleteContentInCartExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public DeleteContentInCartExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

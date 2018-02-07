namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class NotExistProductInDataBaseExceptions : Exception
    {
        public NotExistProductInDataBaseExceptions()
            : base()
        {
        }

        public NotExistProductInDataBaseExceptions(string message)
            : base(message)
        {
        }

        public NotExistProductInDataBaseExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public NotExistProductInDataBaseExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

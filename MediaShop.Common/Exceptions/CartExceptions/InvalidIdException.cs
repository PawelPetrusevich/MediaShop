namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class InvalidIdException : Exception
    {
        public InvalidIdException()
           : base()
        {
        }

        public InvalidIdException(string message)
            : base(message)
        {
        }

        public InvalidIdException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InvalidIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

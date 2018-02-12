using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.PaymentExceptions
{
    [Serializable]
    public class InvalideDecerializableExceptions : Exception
    {
        public InvalideDecerializableExceptions()
            : base()
        {
        }

        public InvalideDecerializableExceptions(string message)
            : base(message)
        {
        }

        public InvalideDecerializableExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InvalideDecerializableExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

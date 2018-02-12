namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AddContentInCartExceptions : Exception
    {
        public AddContentInCartExceptions()
            : base()
        {
        }

        public AddContentInCartExceptions(string message)
            : base(message)
        {
        }

        public AddContentInCartExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AddContentInCartExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

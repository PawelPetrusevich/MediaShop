namespace MediaShop.Common.Exceptions.CartExceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class CartExceptions
    /// </summary>
    [Serializable]
    public class ExistContentInCartExceptions : Exception
    {
        public ExistContentInCartExceptions()
            : base()
        {
        }

        public ExistContentInCartExceptions(string message)
            : base(message)
        {
        }

        public ExistContentInCartExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ExistContentInCartExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

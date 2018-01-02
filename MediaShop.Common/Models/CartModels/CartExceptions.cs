namespace MediaShop.Common.Models.CartModels
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class CartExceptions
    /// </summary>
    [Serializable]
    public class CartExceptions : Exception
    {
        public CartExceptions()
            : base()
        {
        }

        public CartExceptions(string message)
            : base(message)
        {
        }

        public CartExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }

        public CartExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

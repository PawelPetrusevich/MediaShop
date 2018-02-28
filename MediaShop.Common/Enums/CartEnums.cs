namespace MediaShop.Common.Enums
{
    /// <summary>
    /// Class enums
    /// </summary>
    public class CartEnums
    {
        /// <summary>
        /// Enum for discribe ContentCarts stat
        /// </summary>
        public enum StateCartContent : byte
        {
            None = 0,

            /// <summary>
            /// content add in cart but did not Bought
            /// </summary>
            InCart = 1,

            /// <summary>
            /// content object Bought but did not  Paid
            /// </summary>
            InBought = 2,

            /// <summary>
            /// content object Paid
            /// </summary>
            InPaid = 3
        }
    }
}

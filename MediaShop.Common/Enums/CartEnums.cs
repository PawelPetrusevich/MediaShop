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
            /// <summary>
            /// content add in cart but did not Bought
            /// </summary>
            InCart = 0,

            /// <summary>
            /// content object Bought but did not  Paid
            /// </summary>
            InBought = 1,

            /// <summary>
            /// content object Paid
            /// </summary>
            InPaid = 2
        }
    }
}

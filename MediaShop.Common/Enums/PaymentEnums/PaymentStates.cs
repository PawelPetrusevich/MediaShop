namespace MediaShop.Common.Enums.PaymentEnums
{
    /// <summary>
    /// State of Payment
    /// </summary>
    public enum PaymentStates : byte
    {
        /// <summary>
        /// Payment created but not confirmed
        /// </summary>
        Сreated = 0,

        /// <summary>
        /// Payment confirmed succesfully
        /// </summary>
        Approved = 1
    }
}

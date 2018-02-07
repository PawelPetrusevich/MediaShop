namespace MediaShop.Common.Enums.PaymentEnums
{
    /// <summary>
    /// State of Payment
    /// </summary>
    public enum PaymentStates : byte
    {
        /// <summary>
        /// State Payment by default
        /// </summary>
        None = 0,

        /// <summary>
        /// Payment created but not confirmed
        /// </summary>
        Created = 1,

        /// <summary>
        /// Payment confirmed succesfully
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Payment is failed
        /// </summary>
        Failed = 3
    }
}

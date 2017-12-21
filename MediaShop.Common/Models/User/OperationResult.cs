namespace MediaShop.Common.Models.User
{
    /// <summary>
    /// Describes, is operation executed successfully
    /// </summary>
    public class OperationResult
    {
        public OperationResult(bool hasError, string message = null)
        {
            this.HasError = hasError;
            this.Message = message;
        }

        public bool HasError { get; }

        public string Message { get; }
    }
}
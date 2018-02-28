using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when add to repository fail
    /// </summary>
    [Serializable]
    public class AddSettingsException : Exception
    {
        public AddSettingsException()
        {
        }

        public AddSettingsException(string message) : base(message)
        {
        }

        public AddSettingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    /// <summary>
    /// Exception arising when update repository fail
    /// </summary>
    [Serializable]
    public class UpdateSettingsException : Exception
    {
        public UpdateSettingsException()
        {
        }

        public UpdateSettingsException(string message) : base(message)
        {
        }

        public UpdateSettingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdateSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
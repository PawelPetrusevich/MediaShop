using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    [Serializable]
    public class ModiffySettingsException : Exception
    {
        public ModiffySettingsException()
        {
        }

        public ModiffySettingsException(string message) : base(message)
        {
        }

        public ModiffySettingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModiffySettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System.Runtime.Serialization;

namespace AzposAdminApi.Exceptions
{
    public class KnownException : Exception
    {
        public bool IsSaveToLog { get; set; }

        public KnownException(string? message, bool saveToLog = true) : base(message)
        {
            IsSaveToLog = saveToLog;
        }

        public KnownException(string? message, Exception? innerException,bool saveToLog = true) : base(message, innerException)
        {
            IsSaveToLog = saveToLog;
        }

        protected KnownException(SerializationInfo info, StreamingContext context, bool saveToLog = true) : base(info, context)
        {
            IsSaveToLog = saveToLog;
        }
    }
}
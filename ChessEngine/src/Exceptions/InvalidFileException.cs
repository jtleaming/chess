using System;
using System.Runtime.Serialization;

namespace ChessEngine.Exceptions
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException()
        {
        }
        public InvalidFileException(string message) : base(message)
        {
        }
        public InvalidFileException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected InvalidFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
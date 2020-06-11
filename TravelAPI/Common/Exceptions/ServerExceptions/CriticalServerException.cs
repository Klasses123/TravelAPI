using System;

namespace TravelAPI.Common.Exceptions.ServerExceptions
{
    public class CriticalServerException : Exception
    {
        public string Description { get; protected set; }
        public Exception OriginalExc { get; protected set; }
        public CriticalServerException(string desc, Exception exception) 
        {
            Description = desc;
            OriginalExc = exception;
        }
    }
}

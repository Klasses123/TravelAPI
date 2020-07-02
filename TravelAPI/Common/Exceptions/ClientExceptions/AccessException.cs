using System;

namespace TravelAPI.Common.Exceptions.ClientExceptions
{
    public class AccessException : Exception
    {
        public AccessException(string desc) : base(desc) { }
    }
}

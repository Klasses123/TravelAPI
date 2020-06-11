using System;

namespace TravelAPI.Common.Exceptions.ClientExceptions
{
    public class NotFoundException : Exception
    {
        public string Description { get; protected set; }
        public NotFoundException(string desc) 
        {
            Description = desc;
        }
    }
}

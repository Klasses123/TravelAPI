using System;
using System.Collections.Generic;

namespace TravelAPI.Common.Exceptions.ClientExceptions
{
    public class MissingParametersException : Exception
    {
        public IList<string> MissingParameters { get; protected set; }
        public MissingParametersException(IList<string> missingParams) 
        {
            MissingParameters = missingParams;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.Common.Interfaces
{
    public interface ILogger
    {
        void Fatal(object message, Exception ex);
        void Error(Exception exception);
        void Error(string description, Exception exception);
        void Error(string message);
        void Error(IEnumerable<Exception> exceptions);
        void Information(string message);
    }
}

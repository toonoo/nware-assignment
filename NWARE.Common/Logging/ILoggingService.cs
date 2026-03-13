using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWARE.Common.Logging
{
    public interface ILoggingService
    {
        Task LogRequestAsync(string method, string path, Dictionary<string, string> headers, string body);
        Task LogResponseAsync(string method, string path, int statusCode, string responseBody, long elapsedMilliseconds);
        Task LogExceptionAsync(string method, string path, Exception exception);
    }
}

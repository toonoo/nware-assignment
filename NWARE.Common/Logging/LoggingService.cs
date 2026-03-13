using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NWARE.Common.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly string _logDirectory;
        private readonly string _logFilePath;

        public LoggingService()
        {
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            EnsureLogDirectoryExists();
            _logFilePath = Path.Combine(_logDirectory, $"api_log_{DateTime.Now:yyyy-MM-dd}.log");
        }

        private void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        public async Task LogRequestAsync(string method, string path, Dictionary<string, string> headers, string body)
        {
            var logEntry = new StringBuilder();
            var separator = new string('=', 80);
            logEntry.AppendLine("\n" + separator);
            logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] REQUEST");
            logEntry.AppendLine(separator);
            logEntry.AppendLine($"Method: {method}");
            logEntry.AppendLine($"Path: {path}");
            
            if (headers != null && headers.Count > 0)
            {
                logEntry.AppendLine("Headers:");
                foreach (var header in headers)
                {
                    var value = header.Key.ToLower().Contains("authorization") ? "***HIDDEN***" : header.Value;
                    logEntry.AppendLine($"  {header.Key}: {value}");
                }
            }

            if (!string.IsNullOrEmpty(body))
            {
                logEntry.AppendLine($"Body: {body}");
            }

            await WriteToFileAsync(logEntry.ToString());
        }

        public async Task LogResponseAsync(string method, string path, int statusCode, string responseBody, long elapsedMilliseconds)
        {
            var logEntry = new StringBuilder();
            var separator = new string('=', 80);
            logEntry.AppendLine($"\n[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] RESPONSE");
            logEntry.AppendLine(separator);
            logEntry.AppendLine($"Method: {method}");
            logEntry.AppendLine($"Path: {path}");
            logEntry.AppendLine($"Status Code: {statusCode}");
            logEntry.AppendLine($"Elapsed Time: {elapsedMilliseconds}ms");

            if (!string.IsNullOrEmpty(responseBody))
            {
                logEntry.AppendLine($"Response Body: {responseBody}");
            }
            logEntry.AppendLine(separator + "\n");

            await WriteToFileAsync(logEntry.ToString());
        }

        public async Task LogExceptionAsync(string method, string path, Exception exception)
        {
            var logEntry = new StringBuilder();
            var separator = new string('=', 80);
            logEntry.AppendLine($"\n[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] EXCEPTION");
            logEntry.AppendLine(separator);
            logEntry.AppendLine($"Method: {method}");
            logEntry.AppendLine($"Path: {path}");
            logEntry.AppendLine($"Exception Type: {exception.GetType().FullName}");
            logEntry.AppendLine($"Message: {exception.Message}");
            logEntry.AppendLine($"StackTrace: {exception.StackTrace}");

            if (exception.InnerException != null)
            {
                logEntry.AppendLine($"Inner Exception: {exception.InnerException.Message}");
            }
            logEntry.AppendLine(separator + "\n");

            await WriteToFileAsync(logEntry.ToString());
        }

        private async Task WriteToFileAsync(string content)
        {
            try
            {
                await File.AppendAllTextAsync(_logFilePath, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
                Console.WriteLine(content);
            }
        }
    }
}

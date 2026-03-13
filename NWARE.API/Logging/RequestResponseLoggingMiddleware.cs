using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NWARE.Common.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NWARE.API.Logging
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggingService _loggingService;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggingService loggingService)
        {
            _next = next;
            _loggingService = loggingService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Log Request
            await LogRequest(context);

            // Store original response stream
            var originalBodyStream = context.Response.Body;

            // Create memory stream to capture response
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    // Call next middleware
                    await _next(context);

                    // Log Response
                    stopwatch.Stop();
                    await LogResponse(context, stopwatch.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    await _loggingService.LogExceptionAsync(
                        context.Request.Method,
                        context.Request.Path,
                        ex
                    );
                    throw;
                }
                finally
                {
                    // Copy captured response back to original stream
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            // Read request body
            string requestBody = "";
            if (context.Request.ContentLength > 0)
            {
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; // Reset stream position
                }
            }

            // Extract headers
            var headers = new System.Collections.Generic.Dictionary<string, string>();
            foreach (var header in context.Request.Headers)
            {
                headers[header.Key] = header.Value.ToString();
            }

            await _loggingService.LogRequestAsync(
                context.Request.Method,
                context.Request.Path + context.Request.QueryString,
                headers,
                requestBody
            );
        }

        private async Task LogResponse(HttpContext context, long elapsedMilliseconds)
        {
            // Read response body
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await _loggingService.LogResponseAsync(
                context.Request.Method,
                context.Request.Path + context.Request.QueryString,
                context.Response.StatusCode,
                responseBody,
                elapsedMilliseconds
            );
        }
    }

    // Extension method for middleware
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(
            this IApplicationBuilder builder, ILoggingService loggingService)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>(loggingService);
        }
    }
}

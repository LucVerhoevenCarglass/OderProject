using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oder_infrastructure.Logging;

namespace Oder_infrastructure.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILogger _logger = ApplicationLogging.CreateLogger("Error");

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case ArgumentException _:
                case EntityNotValidException _:
                case InvalidOperationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotAuthorizedException _:
                    code = HttpStatusCode.Forbidden;
                    break;
                case EntityNotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            var error = ErrorBuilder.Build(exception, code, context.Request);
            _logger.LogError("REST call threw exception [" + error.UniqueErrorId + "] , request=" + FullUrl(context.Request), exception);

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
        
        private static string FullUrl(HttpRequest request)
        {
            return request.Method + " " + request.Path + " " + request.QueryString;
        }
    }

    public class ErrorDto
    {
        public string UniqueErrorId { get; set; }
        public string Message { get; set; }
        public int HttpStatusCode { get; set; }
    }

    public static class ErrorBuilder
    {
        public static ErrorDto Build(Exception exception, HttpStatusCode httpStatusCode, HttpRequest request)
        {
            return new ErrorDto
            {
                HttpStatusCode = (int) httpStatusCode,
                Message = GetMessage(exception),
                UniqueErrorId = Guid.NewGuid().ToString("N")
            };
        }

        private static string GetMessage(Exception exception)
        {
            if (exception.Message != null)
            {
                return exception.Message;
            }

            return "Something went wrong, please review your request and try again. If the error persists, " +
                   "please contact us at somethingwentwrong@mywebsite.com";
        }
    }
}
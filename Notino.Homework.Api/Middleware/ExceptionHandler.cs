using Newtonsoft.Json;
using Notino.Converter.Exceptions;
using Notino.Homework.Api.Models;
using System.Net;

namespace Notino.Homework.Api.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new Error
            {
                Success = false
            };
            switch (exception)
            {
                case FileFormatNotSupportedException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server error.";
                    break;
            }
            _logger.LogError(exception.Message);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}

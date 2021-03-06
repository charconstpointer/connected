using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Connected.Api.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Connected.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                ApplicationException _ => (int) HttpStatusCode.BadRequest,
                MyCustomValidationException _ => (int) HttpStatusCode.BadRequest,
                _ => 500
            };

            context.Response.ContentType = "application/json";
            object response;
            string json;
            if (exception is MyCustomValidationException)
            {
                response = new {Message = exception.Data["Errors"], Errors = exception.Data["Errors"]};
                json = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }

            response = new {exception.Message, Errors = exception.Data["Errors"]};
            json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
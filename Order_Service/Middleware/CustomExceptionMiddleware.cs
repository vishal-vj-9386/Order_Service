using System.Net;
using System;
using Newtonsoft.Json;

namespace Order_Service.API.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                int statusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonConvert.SerializeObject(new
                {
                    StatusCode = statusCode,
                    ErrorMessage = ex.Message
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);
            }
        }
    }
}

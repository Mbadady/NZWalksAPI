using System;
using System.Net;
using System.Text.Json;

namespace NZWalks.API.Middleware
{
	public class ExceptionHandlerMiddleware
	{
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
		{
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                logger.LogError(ex, $"{errorId} : {ex.Message}");

                // Return a custom error

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong and we are looking to resolve it"
                };

                //JsonSerializer.Serialize(error);

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
	}
}


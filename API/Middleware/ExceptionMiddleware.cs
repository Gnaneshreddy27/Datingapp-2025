using System;
using System.Net;
using System.Text.Json;


using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next,
 ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var json = JsonSerializer.Serialize(response, _jsonOptions);
            await context.Response.WriteAsync(json);
        }
    }

}

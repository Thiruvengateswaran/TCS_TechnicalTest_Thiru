﻿public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = "An unexpected error occurred.",
                Details = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

// Register middleware in `Program.cs` or `Startup.cs`


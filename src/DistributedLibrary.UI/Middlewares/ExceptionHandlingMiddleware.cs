using System.Net.Mime;
using System.Net;
using System.Text.Json;
using DistributedLibrary.Shared.Dto;

namespace DistributedLibrary.UI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _serializerSettings;

    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        JsonSerializerOptions serializerSettings,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _serializerSettings = serializerSettings;

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
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        _logger.LogError(exception, exception.Message);

        var response = ResponseDto.Error("Internal Server errors. Check Logs!");
        response.SetMessage(exception.Message);

        var result = JsonSerializer.Serialize(response, _serializerSettings);
        await context.Response.WriteAsync(result);
    }
}
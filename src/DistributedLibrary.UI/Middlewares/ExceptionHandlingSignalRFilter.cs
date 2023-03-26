using Microsoft.AspNetCore.SignalR;

namespace DistributedLibrary.UI.Middlewares
{
    public class ExceptionHandlingSignalRFilter : IHubFilter
    {
        private readonly ILogger<ExceptionHandlingSignalRFilter> _logger;

        public ExceptionHandlingSignalRFilter(ILogger<ExceptionHandlingSignalRFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object> InvokeMethodAsync(
            HubInvocationContext invocationContext, 
            Func<HubInvocationContext, ValueTask<object>> next)
        {
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "unhandled exception in SignalR");
                throw;
            }
        }
    }
}

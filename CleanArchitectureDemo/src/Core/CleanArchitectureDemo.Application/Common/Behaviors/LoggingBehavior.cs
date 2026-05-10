using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureDemo.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Handling {RequestName} with content: {@Request}", requestName, request);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var response = await next();
        stopwatch.Stop();

        _logger.LogInformation(
            "Handled {RequestName} with response: {@Response} in {ElapsedMilliseconds} ms", requestName, response, stopwatch.ElapsedMilliseconds);
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning("Handling {RequestName} took {ElapsedMilliseconds} ms, which is longer than expected.", requestName, stopwatch.ElapsedMilliseconds);
            }
        return response;
    }
}
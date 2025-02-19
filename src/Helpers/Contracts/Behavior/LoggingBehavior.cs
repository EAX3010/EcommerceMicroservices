using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behavior;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger = _logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        Stopwatch timer = new();
        timer.Start();
        var response = await next();
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Milliseconds > 500) // if the request is greater than 500 Milliseconds, then
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                typeof(TRequest).Name, timeTaken.Seconds);

        logger.LogInformation("[END] Handle request={Request} with Response={Response} timeTaken={timeTaken}",
            typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.Milliseconds);
        return response;
    }
}
﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        ILogger<LoggingBehavior<TRequest, TResponse>> logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
        {
            logger = _logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
             typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
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
}

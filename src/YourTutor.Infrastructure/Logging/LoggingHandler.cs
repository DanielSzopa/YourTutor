using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Helpers;

namespace YourTutor.Infrastructure.Logging
{
    public class LoggingHandler<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingHandler<TRequest, TResponse>> _logger;

        public LoggingHandler(ILogger<LoggingHandler<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation(AppLogEvent.RequestLog, "Starting request {@RequestName}", typeof(TRequest).Name);

            var result = await next();

            _logger.LogInformation(AppLogEvent.RequestLog, "Completed request {@RequestName}", typeof(TRequest).Name);

            return result;
        }
    }
}



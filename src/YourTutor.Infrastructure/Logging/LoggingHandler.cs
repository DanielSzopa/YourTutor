using MediatR;
using Microsoft.Extensions.Logging;
using YourTutor.Application.Abstractions;

namespace YourTutor.Infrastructure.Logging
{
    public class LoggingHandler<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingHandler<TRequest, TResponse>> _logger;
        private readonly IClock _clock;

        public LoggingHandler(ILogger<LoggingHandler<TRequest, TResponse>> logger, IClock clock)
        {
            _logger = logger;
            _clock = clock;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting request {@RequestName}, {@DateTime}", typeof(TRequest).Name, _clock.Now);

            var result = await next();

            _logger.LogInformation("Completed request {@RequestName}, {@DateTime}", typeof(TRequest).Name, _clock.Now);

            return result;
        }
    }
}



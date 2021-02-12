using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Behaviors
{
    internal class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestFullName = typeof(TRequest).FullName;
            _logger.LogInformation($"CQRS Request:{DateTime.UtcNow}", request);
            return Task.CompletedTask;
        }
    }
}

using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Payment.Application;
using Payment.Application.Behaviors;
using Payment.Application.PaymentProcess.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public static class Installer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));


            services.AddMediatR(typeof(Result).Assembly);

            return services;
        }
    }
}

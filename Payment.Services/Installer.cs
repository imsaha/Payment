using Microsoft.Extensions.DependencyInjection;
using Payment.Application.PaymentGateways;
using Payment.Services;
using System;

namespace Payment
{
    public static class Installer
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services)
        {
            services.AddTransient<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddTransient<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddTransient<IPremiumPaymentGateway, PremiumPaymentGateway>();

            return services;
        }
    }
}

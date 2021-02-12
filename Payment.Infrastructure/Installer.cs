using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment.Application;
using Payment.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public static class Installer
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           Action<DbContextOptionsBuilder> optionBuilder,
           ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            services.AddDbContext<PaymentDbContext>(optionBuilder, serviceLifetime);
            services.AddTransient<IDataContext, PaymentDbContext>();


            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}

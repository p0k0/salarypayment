using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace salary.host.handler
{
    public static class ServiceCollectionExtension
    {
        public static void RegisteHostHandler(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetEmployeeHandler));
            services.AddMediatR(typeof(GetEmployeeWithMaxHourlyRateHandler));
            services.AddMediatR(typeof(GetManyEmployeeHandler));
            services.AddMediatR(typeof(GetTotalSalarySumHandler));
            services.AddMediatR(typeof(SaveEmployeeHandler));
        }
    }
}
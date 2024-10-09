using CRM_App_Core.Services;
using CRM_App_Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CRM_App_Service.Quartz
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.ConfigureOptions<CheckMeetingsJobSetup>();

            services.AddScoped<IMeetingService, MeetingService>();
            return services;
        }
    }
}

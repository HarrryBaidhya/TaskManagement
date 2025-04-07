using Blood.Business.LoginUser;
using Blood.infrastructure;
using Blood.Infrastructure.LoginInfra;
using TaskManagement.Interface;

namespace BloodManagement.ServicesExtensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            ///Add services
            //services.AddScoped<IAgent, AgentBusiness>();
            //services.AddScoped<IAgentRepo, AgentRepo>();
            services.AddScoped<ILoginBusiness, LoginBusiness>();
            services.AddScoped<ILoginRepo, LoginRepo>();
            //services.AddScoped<ITaskManagment, TaskMangement>();

            return services;
        }

        /// <summary>
        /// Adds the hosted services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// Configures the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
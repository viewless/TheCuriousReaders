using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCuriousReaders.Services.Configurations;

namespace TheCuriousReaders.API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        public static AuthenticationConfiguration GetAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfigurationSection = configuration.GetSection("AuthenticationConfiguration");
            services.Configure<AuthenticationConfiguration>(authConfigurationSection);

            return authConfigurationSection.Get<AuthenticationConfiguration>();
        }

        public static UserSubscriptionConfiguration GetUserSubscriptionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfigurationSection = configuration.GetSection("UserSubscriptionConfiguration");
            services.Configure<UserSubscriptionConfiguration>(authConfigurationSection);

            return authConfigurationSection.Get<UserSubscriptionConfiguration>();
        }

        public static string GetAzureConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("AzureBlobStorageConnection");
        }
    }
}

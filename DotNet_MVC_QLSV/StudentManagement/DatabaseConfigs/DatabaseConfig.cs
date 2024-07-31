using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Student.Data;

namespace Student.DatabaseConfig
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StudentContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}
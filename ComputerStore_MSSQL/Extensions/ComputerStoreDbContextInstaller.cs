using ComputerStore_MSSQL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore_MSSQL.Extensions
{
    public static class ComputerStoreDbContextInstaller
    {
        public static IServiceCollection AddComputerStoreDataBase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ComputerStoreDbContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("ComputerStore_MSSQL")));

            return services;
        }
    }
}

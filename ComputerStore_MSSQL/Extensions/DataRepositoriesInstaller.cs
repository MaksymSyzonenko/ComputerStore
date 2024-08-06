using ComputerStore_MSSQL.Data.Repositories.Cart;
using ComputerStore_MSSQL.Data.Repositories.Factory;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.Repositories.Review;
using ComputerStore_MSSQL.Data.Repositories.Transaction;
using ComputerStore_MSSQL.Data.Repositories.User;
using ComputerStore_MSSQL.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore_MSSQL.Extensions
{
    public static class DataRepositoriesInstaller
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ICartRepository, CartRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<IReviewRepository, ReviewRepository>();

            services
                .AddSingleton<IRepositoryFactory, RepositoryFactory>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

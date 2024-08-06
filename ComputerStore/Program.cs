using ComputerStore_MSSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ComputerStore.Services.Cache.User;
using ComputerStore.Services.Logger;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data;
using ComputerStore.Services.Register;
using ComputerStore.ViewModels;
using ComputerStore.Models.DTO;
using ComputerStore.Services.Review;
using ComputerStore.Services.Cart;

namespace ComputerStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
            //builder.WebHost.UseUrls($"http://*:{port}");
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("HostingConnection");
            builder.Services.AddComputerStoreDataBase(connectionString!);
            builder.Services.AddSingleton<ILoggerService>(provider => new FileLoggerService("logs.json"));
            builder.Services.AddDataRepositories();
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<ComputerStoreDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<UserEntity>>();
            builder.Services.AddScoped<UserCacheService>();
            builder.Services.AddScoped<IRegisterService<RegisterViewModel, RegisterResultDto>, RegisterService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerPolicy", policy =>
                {
                    policy.RequireRole("Manager", "Admin");
                });
            });
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var services = scope.ServiceProvider;
                await EnsureRolesAsync(services, "User");
                await EnsureRolesAsync(services, "Manager");
                await EnsureRolesAsync(services, "Admin");
                await EnsureDefaultUserAsync(services);
            }

            async Task EnsureRolesAsync(IServiceProvider services, string roleName)
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            async Task EnsureDefaultUserAsync(IServiceProvider services)
            {
                var userManager = services.GetRequiredService<UserManager<UserEntity>>();
                var user = await userManager.FindByNameAsync("AdminMaks");
                if (user == null)
                {
                    user = new UserEntity
                    {
                        FirstName = "Maks",
                        LastName = "Syzonenko",
                        UserName = "AdminMaks",
                        Email = "Maks@gmail.com",
                    };
                    await userManager.CreateAsync(user, "Admin123123.");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorLoggingMiddleware>();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
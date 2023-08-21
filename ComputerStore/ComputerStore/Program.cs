using ComputerStore.DataBaseInteraction;
using ComputerStore.DataRepositories;
using ComputerStore.Interfaces;
using ComputerStore.Middlewares;
using ComputerStore.Models;
using ComputerStore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddSingleton<ILoggerService>(provider => new FileLoggerService("logs.json"));
            builder.Services.AddDbContext<ComputerStoreDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<CartRepository>();
            builder.Services.AddScoped<IRepository<Product, Guid>, ProductRepository>();
            builder.Services.AddScoped<IRepository<Review, Guid>, ReviewRepository>();
            builder.Services.AddScoped<IRepository<Transaction, Guid>, TransactionRepository>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ComputerStoreDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<UserCacheService>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagePolicy", policy =>
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
                var userManager = services.GetRequiredService<UserManager<User>>();
                var user = await userManager.FindByNameAsync("AdminMaks");
                if (user == null)
                {
                    user = new User
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
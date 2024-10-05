using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.DataBase;
using ToDo.Domain.Entity;

namespace ToDo.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("MariaDbLocal"),
                new MySqlServerVersion(new Version(11, 5, 2))));
        services.AddIdentity<User, Role>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 4;
                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            })
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Admin/AccessDenied";
            options.Cookie.Name = "WebAppIdentityCookie";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.LoginPath = "/Admin/Login";
            options.SlidingExpiration = true;
        });
        services.AddSession(option => { option.IdleTimeout = TimeSpan.FromHours(1); });

        return services;
    }
}
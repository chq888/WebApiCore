//using Application.Common.Interfaces;
//using Infrastructure.Identity;
//using Infrastructure.Persistence;
//using Infrastructure.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Infrastructure
//{
//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
//        {
//            // Add DB Context.
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(
//                    configuration.GetConnectionString("DefaultConnection"),
//                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

//            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

//            // Add Services.
//            services.AddScoped<IDomainEventService, DomainEventService>();
//            services.AddTransient<IDateTimeService, DateTimeService>();
//            services.AddTransient<IIdentityService, IdentityService>();

//            // Add Identity.
//            services.AddDefaultIdentity<ApplicationUser>()
//                    .AddRoles<IdentityRole>()
//                    .AddEntityFrameworkStores<ApplicationDbContext>();

//            services.AddIdentityServer()
//                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

//            services.AddAuthentication()
//                    .AddIdentityServerJwt();

//            services.AddAuthorization(options =>
//            {
//                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
//            });

//            return services;
//        }
//    }
//}
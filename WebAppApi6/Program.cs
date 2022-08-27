using API.Services;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebAppApi6.CustomFormatters;
using WebAppApi6.Middleware;
using WebAppApi6.Services;

namespace WebAppApi6
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            builder.Services.AddHttpContextAccessor();
            AddSwaggerDoc(builder.Services);

            builder.Services.AddApplication(builder.Configuration);
            //builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            builder.Services.AddSingleton<IBookService, InMemoryBookService>();

            // Customise default API behaviour
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                //tells the server that if the client tries to negotiate for the media type the server doesn’t support,
                //it should return the 406 Not Acceptable status code.
                options.ReturnHttpNotAcceptable = true;

            }).AddXmlSerializerFormatters().AddCsvOutputFormatter();
            //.AddMvcOptions(options => options.OutputFormatters.Add(new CsvOutputFormatter()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            //other code omitted
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                //options.Conventions.Controller<WebAppApi6.Controllers.V2.ValuesController>().HasApiVersion(new ApiVersion(2, 0));

                options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader("ver"),
                    new UrlSegmentApiVersionReader());
            });

            //        // added to the web api configuration in the application setup
            //        var constraintResolver = new DefaultInlineConstraintResolver()
            //        {
            //            ConstraintMap =
            //{
            //    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
            //}
            //        };
            //        services.MapHttpAttributeRoutes(constraintResolver);

            //        IServiceProvider serviceProvider = services.BuildServiceProvider();

            //        services.AddOptions();


            ////add swagger openapi
            //builder.Services.AddSwaggerGen(sa =>
            //{
            //    sa.SwaggerDoc("WebOpenApiSpec",
            //        new Microsoft.OpenApi.Models.OpenApiInfo()
            //        {
            //            Title = "Library api",
            //            Version = "1"
            //        });

            //    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            //    sa.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlCommentsFile));
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR.LeaveManagement.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.MapControllers();

            //app.UseSwagger();
            //app.UseSwaggerUI(sa =>
            //{
            //    sa.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API");
            //    sa.RoutePrefix = "apidoc";
            //});

            //await app.SeedDatabaseAsync();

            app.Run();
        }

        private static void AddSwaggerDoc(IServiceCollection services)
        {
            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " Management Api",

                });

            });
        }

        public static IServiceCollection AddIdentityServices(IServiceCollection services, IConfiguration config)
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options => {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = key,
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //        options.Events = new JwtBearerEvents
            //        {
            //            OnMessageReceived = context =>
            //            {
            //                var accessToken = context.Request.Query["access_token"];
            //                var path = context.HttpContext.Request.Path;
            //                if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chats")))
            //                {
            //                    context.Token = accessToken;
            //                }
            //                return Task.CompletedTask;
            //            }
            //        };
            //    });
            //services.AddAuthorization(options => {
            //    options.AddPolicy("IsActivityHost", policy => {
            //        policy.Requirements.Add(new IsHostRequirement());
            //    });
            //});
            //services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();

            return services;
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Reflection;

namespace WebAppApi6
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
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
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //app.UseSwagger();
            //app.UseSwaggerUI(sa =>
            //{
            //    sa.SwaggerEndpoint("/swagger/WebOpenApiSpec/swagger.json", "Web API");
            //    sa.RoutePrefix = "apidoc";
            //});


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
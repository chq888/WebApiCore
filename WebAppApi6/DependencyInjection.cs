using System.Collections.Generic;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using API.Services;

namespace API
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add services from API layer to DI container.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Application configuration.</param>
        public static IServiceCollection AddWebUI(this IServiceCollection services, IConfiguration configuration)
        {
            

            return services;
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Core.IRepository;
using UserData.Core.IServices;
using UserData.Repository;
using UserData.Services;


namespace Utility
{
    public class DIResolver
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IServices, Services>();
            services.AddScoped<, Repository>();
        }
    }
}

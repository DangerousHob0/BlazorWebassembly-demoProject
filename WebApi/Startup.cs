using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.ResponseCompression;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(opts =>
            {
                opts.UseSqlServer(Configuration[
                    "ConnectionStrings:TasksConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddResponseCompression(opts => {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, DataContext context)
    {
        app.UseDeveloperExceptionPage();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToClientSideBlazor<BlazorWebAssembly.Startup>
            ("{*path:nonfile}", "index.html");
        });
        app.Map("", opts => opts.UseClientSideBlazorFiles<BlazorWebAssembly.Startup>());

        SeedData.SeedDatabase(context);
    }
}
}

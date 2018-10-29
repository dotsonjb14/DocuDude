﻿using docudude.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using THT.AWS.Abstractions.Options;
using THT.AWS.Abstractions.S3;

namespace docudude
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<S3Options>(Configuration.GetSection("s3"));

            if (Configuration.GetValue("IsLocal", false))
            {
                services.AddTransient<IFileWrapper, LocalFileWrapper>();
            }
            else
            {
                services.AddTransient<IFileWrapper, S3FileWrapper>();
            }

            services.AddTransient<DocumentRepository>();
            services.AddTransient<S3Repository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
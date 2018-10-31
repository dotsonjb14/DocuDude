using System;
using docudude.Models;
using docudude.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using THT.AWS.Abstractions.Credentials;
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
            
            services.AddTransient<ICrendentialsManager, CrendentialsManager>();

            if (Configuration.GetValue("IsLocal", false))
            {
                services.AddTransient<IFileWrapper, LocalFileWrapper>();
            }
            else
            {
                services.AddTransient<IFileWrapper, S3FileWrapper>();
            }

            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IS3Repository, S3Repository>();
            services.AddSingleton<Whitelists>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Whitelists whitelists, IOptions<S3Options> options)
        {
            Console.WriteLine(options.Value.Region);
            whitelists.SetWhiteList(
                Environment.GetEnvironmentVariable("PDF_WHITELIST"),
                WhiteListType.PDF
            );

            whitelists.SetWhiteList(
                Environment.GetEnvironmentVariable("IMAGE_WHITELIST"),
                WhiteListType.Image
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

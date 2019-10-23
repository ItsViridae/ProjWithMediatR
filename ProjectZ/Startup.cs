using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectZ.Data;
using Vinformatix.Api.Swagger;
using Vinformatix.Authentication;
using Vinformatix.Infrastructure.Services;

namespace ProjectZ
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureMediatr(typeof(Startup));
            services.ConfigureFluentValidation<Startup>();
            services.ConfigureMapping(typeof(Startup));
            services.SetupTokenAuthentication(Configuration);

            services.AddScoped<ChildValidator, ChildValidator>();
            services.AddScoped<ParentValidator, ParentValidator>();

            services.AddSwaggerDocumentation();

            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration["ConnectionString:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
                
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public class ParentValidator
    {
        public ParentValidator(ChildValidator childValidator)
        {
            
        }
    }

    public class ChildValidator
    {
        public ChildValidator()
        {
            
        }
    }
}

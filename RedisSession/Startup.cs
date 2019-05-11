using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RedisSession
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
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("OpenSourceRoadshowConn");
                options.InstanceName = "OpenSourceRoadshow";
            });

            // services.AddDistributedSqlServerCache(options =>
            // {
            //     options.ConnectionString = Configuration.GetConnectionString("OpenSourceRoadshowSQLConn");
            //     options.SchemaName = "dbo";
            //     options.TableName = "OpenSourceRoadshow";
            // });

            services.AddSession(options => {   
                options.IdleTimeout = TimeSpan.FromSeconds(50); 
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();  
            app.UseMvc();
        }
    }
}

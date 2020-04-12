using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rating_services.Models;
using Rating_services.Repository;

namespace Rating_services
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                        var hostname = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "localhost";
            var password = Environment.GetEnvironmentVariable("SQLSERVER_SA_PASSWORD") ?? "ThisIsPass1@";
            //var connString = $"Data Source={hostname};Initial Catalog=Rating_Context;User ID=sa;Password={password};MultipleActiveResultSets=true;";
            var connString = $"Server=db;Initial Catalog=Rating_Context;User ID=sa;Password={password};";
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrder_detailRepository, Order_detailRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
            services.AddDbContext<Rating_Context>(options => options.UseSqlServer(connString));
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}

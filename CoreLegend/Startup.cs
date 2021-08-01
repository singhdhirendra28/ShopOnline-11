using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLegend.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreLegend
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
            //var connection = @"Data Source=DESKTOP-3OM620D\SQLEXPRESS;Initial Catalog=HumanResource;Integrated Security=True;MultipleActiveResultSets=True";

            //var connection = @"Server =DESKTOP-3OM620D\SQLEXPRESS; Database = HumanResource; Trusted_Connection = True;";            
            //services.AddDbContext<EmployeeDataContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<EmployeeDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc();
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

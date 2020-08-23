using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coal.Storing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coal.Domain {
     public class Startup {
          public Startup (IConfiguration configuration) 
          {
               Configuration = configuration;
          }

          public IConfiguration Configuration { get; }

          // This method gets called by the runtime. Use this method to add services to the container.
          public void ConfigureServices (IServiceCollection services) 
          {
               services.AddControllers ();
               services.AddDbContext<CoalDbContext>(options =>
               {
                    options.UseSqlServer(Configuration.GetConnectionString("mssql")); //recommended
                    //options.UseSqlServer(Configuration["dataconnect:mssql"]); //all other config
               });
               services.AddCors(options => 
               {
                    options.AddDefaultPolicy(poli =>
                    {
                         poli.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
               });
          }

          // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
          public void Configure (IApplicationBuilder app, IWebHostEnvironment env) 
          {
               if (env.IsDevelopment ()) {
                    app.UseDeveloperExceptionPage ();
               }

               //app.UseHttpsRedirection ();
               
               app.UseCors(); 

               app.UseRouting();

               app.UseAuthorization();

               app.UseAuthentication();

               app.UseEndpoints (endpoints => {
                    endpoints.MapControllers();
               });
          }
     }
}
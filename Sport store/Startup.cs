using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sport_store.Models;
using Microsoft.EntityFrameworkCore;

namespace Sport_store
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}/Strona{productPage}",
                    defaults: new { Controller = "Product", action = "List" });
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Strona{productPage}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1 });
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { Controller = "Product", action = "List" , productPage = 1});
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { Controller = "Product", action = "List", productPage = 1 });
                endpoints.MapControllerRoute(
                    name: default,
                    pattern: "{controller=Product}/{action=List}/{id?}");

            });
            SeedData.EnsurePopulated(app);
        }
    }
}

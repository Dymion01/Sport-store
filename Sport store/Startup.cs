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
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

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


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SportStore API",
                    Description = "SportStore ASP.NET Core Web API",
                    
                    Contact = new OpenApiContact
                    {
                        Name = "Damian Skorupa",
                        Email = "nietwojasprawa@gmail.com",
                        
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseSwagger();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api";
            });

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
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}

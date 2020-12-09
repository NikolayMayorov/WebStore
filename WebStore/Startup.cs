using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infastrature.Interfaces;
using WebStore.Infastrature.Services;

namespace WebStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //  services.AddMvc(); // core 2.0
            services.AddControllersWithViews();


            //services.AddTransient  создается каждый раз свой экземпляр
            //services.AddScoped - один экземаля на область видимости

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseWelcomePage("/welcome");

            app.UseStaticFiles();
            app.UseDefaultFiles(); //!!!!!!

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/helloworld",
                    async context =>
                    {
                        await context.Response.WriteAsync(text: Configuration.GetSection("Gretigns").Value);
                    });


                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
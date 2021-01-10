using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.DomainCore.Entities.Identity;
using WebStore.Expansion;
using WebStore.Infastrature.Interfaces;
using WebStore.Infastrature.Services.InCookie;
using WebStore.Infastrature.Services.InSQL;

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

            services.AddDbContext<WebStoreDB>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<WebStoreDB>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;

                opt.Password.RequireNonAlphanumeric = false;

                opt.Password.RequireUppercase = false;

                opt.Password.RequireLowercase = false;

                opt.User.RequireUniqueEmail = false;
            });


            //services.Configure<CookieOptions>(opt =>
            //{
            //    opt.HttpOnly = true;
            //});

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.Cookie.HttpOnly = true;

                opt.SlidingExpiration = true;
            });


            // services.AddIdentityCore

            //services.AddTransient  создаетс€ каждый раз свой экземпл€р  (этот способ приортнее дл€ многопотока)
            //services.AddScoped - один экземал€ на область видимости
            //services.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); //создание происходит в момент обращение к IEmployeesData (ленива€ инициализаци€)


            services.AddTransient<WebStoreDBInit>();

           

            services.RegService();
            services.AddTransient<ICartService, InCookieCartService>();

            services.AddScoped<IOrderService, InSqlOrderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInit webStoreDbInit, IProductData productData)
        {
            webStoreDbInit.Init();


            var product = productData.GetProductById(1);
            int brandId = product?.BrandId ?? 0;


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseWelcomePage("/welcome");

            app.UseStaticFiles();
            app.UseDefaultFiles(); //!!!!!!

            app.UseCookiePolicy();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/helloworld",
                    async context =>
                    {
                        await context.Response.WriteAsync(text: Configuration.GetSection("Gretigns").Value);
                    });

                //endpoints.MapControllerRoute(
                //    name: "areas",
                //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                //);


                endpoints.MapAreaControllerRoute(
                    "areas",
                    "Admin",
                    "Admin/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapAreaControllerRoute(
                    "areas",
                    "Moderator",
                    "Moderator/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");



              

            });
           
        }
    }
}
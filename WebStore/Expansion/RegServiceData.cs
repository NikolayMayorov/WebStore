using Microsoft.Extensions.DependencyInjection;
using WebStore.Infastrature.Interfaces;
using WebStore.Infastrature.Services;
using WebStore.Infastrature.Services.InSQL;

namespace WebStore.Expansion
{
    public static class RegServiceData
    {
        public static void RegService(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
           // services.AddSingleton<IProductData, InMemoryProductData>();
           services.AddScoped<IProductData, InSQLProductData>();
        }
    }
}

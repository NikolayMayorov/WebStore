using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Infastrature.Interfaces;
using WebStore.Infastrature.Services;

namespace WebStore.Expansion
{
    public static class RegServiceData
    {
        public static void RegService(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();
        }
    }
}

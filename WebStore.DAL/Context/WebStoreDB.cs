using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.DomainCore.Entities;
using WebStore.DomainCore.Entities.Identity;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options)
        {

        }
    }

    
}

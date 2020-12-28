using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainCore.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDBInit
    {
        private readonly WebStoreDB _webStoreDb;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public WebStoreDBInit(WebStoreDB webStoreDB, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _webStoreDb = webStoreDB;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Init()
        {
            InitAsync().Wait();
        }

        private async Task InitAsync()
        {
            await _webStoreDb.Database.EnsureCreatedAsync();

            if (await _webStoreDb.Products.AnyAsync().ConfigureAwait(false))
                return;

            //await _webStoreDb.Database.MigrateAsync().ConfigureAwait(false);

            if (await _webStoreDb.Sections.CountAsync().ConfigureAwait(false) == 0)
            {
                await using var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false);

                await _webStoreDb.Sections.AddRangeAsync(entities: TestData.Sections).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            if (await _webStoreDb.Brands.CountAsync().ConfigureAwait(false) == 0)
            {
                await using var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false);
                await _webStoreDb.Brands.AddRangeAsync(entities: TestData.Brands).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            if (await _webStoreDb.Products.CountAsync().ConfigureAwait(false) == 0)
            {
                await using var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false);
                await _webStoreDb.Products.AddRangeAsync(entities: TestData.Products).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            await using (var transaction = await _webStoreDb.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await _webStoreDb.Employee.AddRangeAsync(TestData.Employees).ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employee] ON");

                await _webStoreDb.SaveChangesAsync().ConfigureAwait(false);

                await _webStoreDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employee] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }


            if (!await _roleManager.RoleExistsAsync(Role.Administrator))
            {
                await _roleManager.CreateAsync(new Role(){Name = Role.Administrator}).ConfigureAwait(false);
            }

            if (await _userManager.FindByNameAsync(User.DefaultPassword) is null)
            {
                var user = new User()
                {
                    UserName = User.Administrator
                };

                var resultCreateUser = await _userManager.CreateAsync(user, User.DefaultPassword).ConfigureAwait(false);

                if (resultCreateUser.Succeeded)
                    await _userManager.AddToRoleAsync(user, Role.Administrator);
            }
        }
    }
}